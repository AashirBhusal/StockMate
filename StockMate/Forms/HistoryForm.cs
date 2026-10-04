using System;
using System.Collections.Generic;
using System.Text;
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

        // Puts the item and its movements on the clipboard as plain text, to
        // paste into an email or a note.
        // A StockItem and a StockMovement have no base class in common, but
        // both are IReportable, so one list can hold them and the loop below
        // does not need to know which is which.
        private void btnCopy_Click(object sender, EventArgs e)
        {
            List<IReportable> lines = new List<IReportable>();
            lines.Add(_item);

            foreach (StockMovement movement in _movements)
            {
                lines.Add(movement);
            }

            StringBuilder text = new StringBuilder();

            foreach (IReportable line in lines)
            {
                text.AppendLine(line.ToReportLine());
            }

            try
            {
                Clipboard.SetText(text.ToString());
                MessageBox.Show("Copied " + lines.Count + " line(s) to the clipboard.", "StockMate");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not copy to the clipboard." + Environment.NewLine + Environment.NewLine + ex.Message,
                    "StockMate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
