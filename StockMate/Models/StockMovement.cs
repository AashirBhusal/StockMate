using System;

namespace StockMate.Models
{
    // One receipt or issue, read back from the Transactions table.
    // It is not a stock item, so it does not inherit from StockItem, but it
    // can still describe itself as one line of a report. That is why
    // IReportable is an interface rather than a method on StockItem.
    public class StockMovement : IReportable
    {
        public StockMovement(int itemId, string transactionType, int quantity,
                             DateTime transactionDate, string staffName)
        {
            if (quantity <= 0)
                throw new ArgumentException("Movement quantity must be more than zero.");

            ItemId = itemId;
            TransactionType = transactionType;
            Quantity = quantity;
            TransactionDate = transactionDate;
            StaffName = staffName;
        }

        public int TransactionId { get; set; }
        public int ItemId { get; }

        // "Receipt" or "Issue", matching what SaveMovement writes.
        public string TransactionType { get; }
        public int Quantity { get; }
        public DateTime TransactionDate { get; }
        public string StaffName { get; }

        public bool IsReceipt
        {
            get { return TransactionType == "Receipt"; }
        }

        // Positive for stock coming in, negative for stock going out, so a
        // running total can just add these up.
        public int SignedQuantity
        {
            get { return IsReceipt ? Quantity : -Quantity; }
        }

        public string ToReportLine()
        {
            return TransactionDate.ToString("dd/MM/yyyy HH:mm") + " - " + TransactionType
                 + " " + Quantity + " by " + StaffName;
        }
    }
}
