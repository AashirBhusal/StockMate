using System;

namespace StockMate.Models
{
    // Stock that is reused rather than used up, such as kettles or towels.
    // These never expire, but they wear out, so they are checked against the
    // date they were last replaced instead of a use-by date.
    public class DurableItem : StockItem
    {
        private int _replacementMonths = 12;

        public DurableItem(string name, string category, int quantity,
                           int replacementMonths, DateTime lastReplacedDate)
            : base(name, category, quantity)
        {
            ReplacementMonths = replacementMonths;
            LastReplacedDate = lastReplacedDate;
        }

        // How many months this stock should last, for example 24 for towels.
        public int ReplacementMonths
        {
            get { return _replacementMonths; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Replacement months must be at least 1.");
                _replacementMonths = value;
            }
        }

        public DateTime LastReplacedDate { get; set; }

        public override string ItemType
        {
            get { return "Durable"; }
        }

        public DateTime DueForReplacementOn
        {
            get { return LastReplacedDate.Date.AddMonths(ReplacementMonths); }
        }

        public bool IsDueForReplacement
        {
            get { return DateTime.Today >= DueForReplacementOn; }
        }

        // A durable item is a problem if there are too few, or the set is worn out.
        public override bool NeedsAttention()
        {
            return IsLowStock || IsDueForReplacement;
        }

        public override string AttentionMessage()
        {
            if (IsLowStock)
                return Name + " is low: " + QuantityOnHand + " " + Unit + " left.";

            if (IsDueForReplacement)
                return Name + " was due for replacement on "
                     + DueForReplacementOn.ToString("dd/MM/yyyy") + ".";

            return "";
        }

        public override string ToReportLine()
        {
            return base.ToReportLine() + ", replace by " + DueForReplacementOn.ToString("dd/MM/yyyy");
        }
    }
}
