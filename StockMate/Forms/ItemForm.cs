using System;
using System.Windows.Forms;
using StockMate.Models;

namespace StockMate.Forms
{
    // Add a new item, or edit one that already exists.
    // The same form does both: if an item is passed in, it is an edit.
    public partial class ItemForm : Form
    {
        private readonly StockItem _existingItem;

        // The finished item, read by MainForm after the dialog closes.
        public StockItem Item { get; private set; }

        public ItemForm(StockItem existingItem)
        {
            InitializeComponent();
            _existingItem = existingItem;
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            cboType.Items.Add("Consumable");
            cboType.Items.Add("Durable");
            cboType.SelectedIndex = 0;

            if (_existingItem == null)
            {
                Text = "Add item";
                return;
            }

            Text = "Edit item";
            txtName.Text = _existingItem.Name;
            txtCategory.Text = _existingItem.Category;
            txtUnit.Text = _existingItem.Unit;
            numQuantity.Value = _existingItem.QuantityOnHand;
            numReorder.Value = _existingItem.ReorderLevel;
            numCost.Value = _existingItem.UnitCost;
            cboType.Text = _existingItem.ItemType;

            // The type cannot change after the item is created, because that
            // would mean turning one class into another.
            cboType.Enabled = false;

            // Quantity only changes through a receipt or an issue, so it is
            // read-only once the item exists.
            numQuantity.Enabled = false;

            ConsumableItem consumable = _existingItem as ConsumableItem;
            if (consumable != null)
            {
                dtpExpiry.Value = consumable.ExpiryDate;
            }

            DurableItem durable = _existingItem as DurableItem;
            if (durable != null)
            {
                numMonths.Value = durable.ReplacementMonths;
                dtpLastReplaced.Value = durable.LastReplacedDate;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isConsumable = cboType.Text == "Consumable";

            lblExpiry.Visible = isConsumable;
            dtpExpiry.Visible = isConsumable;

            lblMonths.Visible = !isConsumable;
            numMonths.Visible = !isConsumable;
            lblLastReplaced.Visible = !isConsumable;
            dtpLastReplaced.Visible = !isConsumable;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_existingItem == null)
                {
                    Item = CreateNewItem();
                }
                else
                {
                    Item = UpdateExistingItem();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                // The rules live in the classes, so any invalid value throws
                // from there and is shown to the user here.
                MessageBox.Show(ex.Message, "Please check the form",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private StockItem CreateNewItem()
        {
            StockItem item;

            if (cboType.Text == "Consumable")
            {
                item = new ConsumableItem(txtName.Text, txtCategory.Text,
                    (int)numQuantity.Value, dtpExpiry.Value);
            }
            else
            {
                item = new DurableItem(txtName.Text, txtCategory.Text,
                    (int)numQuantity.Value, (int)numMonths.Value, dtpLastReplaced.Value);
            }

            ApplyCommonFields(item);
            return item;
        }

        private StockItem UpdateExistingItem()
        {
            _existingItem.Name = txtName.Text;
            _existingItem.Category = txtCategory.Text;
            ApplyCommonFields(_existingItem);

            ConsumableItem consumable = _existingItem as ConsumableItem;
            if (consumable != null)
            {
                consumable.ExpiryDate = dtpExpiry.Value;
            }

            DurableItem durable = _existingItem as DurableItem;
            if (durable != null)
            {
                durable.ReplacementMonths = (int)numMonths.Value;
                durable.LastReplacedDate = dtpLastReplaced.Value;
            }

            return _existingItem;
        }

        private void ApplyCommonFields(StockItem item)
        {
            item.Unit = string.IsNullOrWhiteSpace(txtUnit.Text) ? "each" : txtUnit.Text;
            item.ReorderLevel = (int)numReorder.Value;
            item.UnitCost = numCost.Value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
