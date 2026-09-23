using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockMate.Models;

namespace StockMate.Forms
{
    // Shows every receipt and issue for one item, newest first.
    // MainForm loads the list from the database and passes it in, so this
    // form, like ItemForm and MovementForm, never touches SQL itself.
    public partial class HistoryForm : Form
    {
        private readonly StockItem _item;
        private readonly List<StockMovement> _movements;

        public HistoryForm(StockItem item, List<StockMovement> movements)
        {
            InitializeComponent();
            _item = item;
            _movements = movements;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            Text = "History - " + _item.Name;

            if (_movements.Count == 0)
            {
                lblSummary.Text = "No receipts or issues have been recorded for this item yet.";
                return;
            }

            int received = 0;
            int issued = 0;
            int netChange = 0;

            foreach (StockMovement movement in _movements)
            {
                grdHistory.Rows.Add(
                    movement.TransactionDate.ToString("dd/MM/yyyy HH:mm"),
                    movement.TransactionType,
                    movement.Quantity,
                    movement.StaffName);

                if (movement.IsReceipt)
                    received += movement.Quantity;
                else
                    issued += movement.Quantity;

                netChange += movement.SignedQuantity;
            }

            lblSummary.Text = _item.QuantityOnHand + " " + _item.Unit + " on hand. "
                + _movements.Count + " movement(s): received " + received
                + ", issued " + issued + " (net " + (netChange >= 0 ? "+" : "") + netChange + ").";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
