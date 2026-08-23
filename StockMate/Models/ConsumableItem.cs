using System;

namespace StockMate.Models
{
    // Stock that gets used up and has a use-by date, such as milk or soap.
    public class ConsumableItem : StockItem
    {
        // Warn this many days before the use-by date.
        public const int ExpiryWarningDays = 30;

        public ConsumableItem(string name, string category, int quantity, DateTime expiryDate)
            : base(name, category, quantity)
        {
            ExpiryDate = expiryDate;
        }

        public DateTime ExpiryDate { get; set; }

        public override string ItemType
        {
            get { return "Consumable"; }
        }

        // Goes negative once the date has passed.
        public int DaysUntilExpiry
        {
            get { return (ExpiryDate.Date - DateTime.Today).Days; }
        }

        public bool IsExpired
        {
            get { return DaysUntilExpiry < 0; }
        }

        public bool IsNearExpiry
        {
            get { return IsExpired == false && DaysUntilExpiry <= ExpiryWarningDays; }
        }

        // A consumable is a problem if it is running out or going out of date.
        public override bool NeedsAttention()
        {
            return IsLowStock || IsNearExpiry || IsExpired;
        }

        // Worst problem first so the user sees the most urgent message.
        public override string AttentionMessage()
        {
            if (IsExpired)
                return Name + " has expired - remove it from the storeroom.";

            if (IsNearExpiry)
                return Name + " expires in " + DaysUntilExpiry + " day(s) - use it first.";

            if (IsLowStock)
                return Name + " is low: " + QuantityOnHand + " " + Unit + " left.";

            return "";
        }

        public override string ToReportLine()
        {
            return base.ToReportLine() + ", expires " + ExpiryDate.ToString("dd/MM/yyyy");
        }
    }
}
