using System;
using System.Windows.Forms;
using StockMate.Models;

namespace StockMate.Forms
{
    // Asks how many units are coming in or going out, and who did it.
    // The same form is used for both, with the wording changed to suit.
    public partial class MovementForm : Form
    {
        private readonly StockItem _item;
        private readonly string _movementType;

        public int Quantity { get; private set; }
        public string StaffName { get; private set; }

        public MovementForm(StockItem item, string movementType)
        {
            InitializeComponent();
            _item = item;
            _movementType = movementType;
        }

        private void MovementForm_Load(object sender, EventArgs e)
        {
            if (_movementType == "Receipt")
            {
                Text = "Record a delivery";
                lblAction.Text = "How many units arrived?";
            }
            else
            {
                Text = "Take stock out";
                lblAction.Text = "How many units are being taken?";
            }

            lblItem.Text = _item.Name + " - " + _item.QuantityOnHand + " " + _item.Unit + " on hand";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStaff.Text))
            {
                MessageBox.Show("Please enter your name.", "StockMate");
                return;
            }

            Quantity = (int)numQuantity.Value;
            StaffName = txtStaff.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
