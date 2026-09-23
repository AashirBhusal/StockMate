using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockMate.Data;
using StockMate.Models;

namespace StockMate.Forms
{
    // The main window: the stock list, the category filter and the warnings.
    public partial class MainForm : Form
    {
        private readonly Database _database;
        private List<StockItem> _items = new List<StockItem>();

        public MainForm(Database database)
        {
            InitializeComponent();
            _database = database;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            try
            {
                _items = _database.GetAllItems();
                LoadCategories();
                ShowItems();
                ShowWarnings();
            }
            catch (Exception ex)
            {
                ShowError("Could not load the stock list.", ex);
            }
        }

        private void LoadCategories()
        {
            string chosen = cboCategory.Text;

            cboCategory.Items.Clear();
            cboCategory.Items.Add(AllCategories);
            foreach (string category in _database.GetCategories())
            {
                cboCategory.Items.Add(category);
            }

            int index = cboCategory.Items.IndexOf(chosen);
            cboCategory.SelectedIndex = index >= 0 ? index : 0;
        }

        private const string AllCategories = "All categories";

        private void ShowItems()
        {
            grdItems.Rows.Clear();

            foreach (StockItem item in _items)
            {
                if (cboCategory.Text != AllCategories && item.Category != cboCategory.Text)
                    continue;

                grdItems.Rows.Add(
                    item.ItemId,
                    item.Name,
                    item.Category,
                    item.ItemType,
                    item.QuantityOnHand,
                    item.Unit,
                    item.ReorderLevel,
                    item.UnitCost.ToString("C"));
            }
        }

        private void ShowWarnings()
        {
            lstWarnings.Items.Clear();

            foreach (StockItem item in _items)
            {
                // This is where polymorphism does the work. The same two calls
                // give different answers depending on whether the item is a
                // ConsumableItem or a DurableItem, and this code does not need
                // to know which one it is holding.
                if (item.NeedsAttention())
                {
                    lstWarnings.Items.Add(item.AttentionMessage());
                }
            }

            if (lstWarnings.Items.Count == 0)
            {
                lstWarnings.Items.Add("Nothing needs attention today.");
            }
        }

        // Finds the object behind the highlighted row in the grid.
        private StockItem GetSelectedItem()
        {
            if (grdItems.CurrentRow == null)
                return null;

            int itemId = (int)grdItems.CurrentRow.Cells[0].Value;

            foreach (StockItem item in _items)
            {
                if (item.ItemId == itemId)
                    return item;
            }

            return null;
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowItems();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemForm form = new ItemForm(null);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _database.InsertItem(form.Item);
                    RefreshAll();
                }
                catch (Exception ex)
                {
                    ShowError("Could not save the new item.", ex);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            StockItem selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Pick an item in the list first.", "StockMate");
                return;
            }

            ItemForm form = new ItemForm(selected);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _database.UpdateItem(form.Item);
                    RefreshAll();
                }
                catch (Exception ex)
                {
                    ShowError("Could not save the changes.", ex);
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            StockItem selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Pick an item in the list first.", "StockMate");
                return;
            }

            try
            {
                List<StockMovement> history = _database.GetHistory(selected.ItemId);
                HistoryForm form = new HistoryForm(selected, history);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowError("Could not load the history.", ex);
            }
        }

        // Turns an item off instead of deleting it. Its Transactions rows are
        // kept, so the history stays correct; it just no longer shows in the list.
        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            StockItem selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Pick an item in the list first.", "StockMate");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Deactivate \"" + selected.Name + "\"?" + Environment.NewLine + Environment.NewLine +
                "It will disappear from the stock list, but its receipts and issues will be kept.",
                "Deactivate item", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
                return;

            try
            {
                selected.IsActive = false;
                _database.UpdateItem(selected);
                RefreshAll();
            }
            catch (Exception ex)
            {
                ShowError("Could not deactivate the item.", ex);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            RecordMovement("Receipt");
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            RecordMovement("Issue");
        }

        private void RecordMovement(string movementType)
        {
            StockItem selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Pick an item in the list first.", "StockMate");
                return;
            }

            MovementForm form = new MovementForm(selected, movementType);

            if (form.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                if (movementType == "Receipt")
                {
                    selected.Receive(form.Quantity);
                }
                else
                {
                    selected.Issue(form.Quantity);
                }

                _database.SaveMovement(selected, movementType, form.Quantity, form.StaffName);
                RefreshAll();
            }
            catch (InsufficientStockException ex)
            {
                // Catching this one on its own means the user gets a clear
                // message about the stock, while any other error is handled
                // separately below.
                MessageBox.Show(ex.Message, "Not enough stock",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ShowError("Could not save the movement.", ex);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(message + Environment.NewLine + Environment.NewLine + ex.Message,
                "StockMate", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
