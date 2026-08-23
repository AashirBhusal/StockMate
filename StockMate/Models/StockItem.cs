using System;

namespace StockMate.Models
{
    // Base class for anything kept in the storeroom.
    // It is abstract because a plain stock item cannot say whether it needs
    // attention today. A consumable looks at its use-by date and a durable
    // looks at its age, so each child class answers that question its own way.
    public abstract class StockItem : IReportable
    {
        private string _name = "";
        private string _category = "";
        private int _reorderLevel;
        private decimal _unitCost;

        protected StockItem(string name, string category, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            Name = name;
            Category = category;
            QuantityOnHand = quantity;
        }

        public int ItemId { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Item name cannot be blank.");
                _name = value.Trim();
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Category cannot be blank.");
                _category = value.Trim();
            }
        }

        // "each", "box", "litre" and so on.
        public string Unit { get; set; } = "each";

        // The setter is private. Other code can read the quantity but cannot
        // just assign a number to it. It only changes through Receive() and
        // Issue(), which check the rules first.
        public int QuantityOnHand { get; private set; }

        public int ReorderLevel
        {
            get { return _reorderLevel; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Reorder level cannot be negative.");
                _reorderLevel = value;
            }
        }

        public decimal UnitCost
        {
            get { return _unitCost; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Unit cost cannot be negative.");
                _unitCost = value;
            }
        }

        public bool IsActive { get; set; } = true;

        public bool IsLowStock
        {
            get { return QuantityOnHand <= ReorderLevel; }
        }

        public decimal TotalValue
        {
            get { return QuantityOnHand * UnitCost; }
        }

        // Each child class must fill these in.
        public abstract string ItemType { get; }
        public abstract bool NeedsAttention();
        public abstract string AttentionMessage();

        // A delivery arrives.
        public void Receive(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Receipt quantity must be more than zero.");

            QuantityOnHand = QuantityOnHand + quantity;
        }

        // Stock is taken out of the storeroom.
        public void Issue(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Issue quantity must be more than zero.");

            if (quantity > QuantityOnHand)
                throw new InsufficientStockException(Name, quantity, QuantityOnHand);

            QuantityOnHand = QuantityOnHand - quantity;
        }

        public virtual string ToReportLine()
        {
            return Name + " (" + ItemType + ") - " + QuantityOnHand + " " + Unit + " on hand";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
