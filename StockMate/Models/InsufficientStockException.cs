using System;

namespace StockMate.Models
{
    // Thrown when someone tries to take out more stock than there is.
    // A custom type means the form can catch this one problem and show a helpful
    // message, while any other error still surfaces normally.
    public class InsufficientStockException : Exception
    {
        public string ItemName { get; }
        public int QuantityRequested { get; }
        public int QuantityAvailable { get; }

        public InsufficientStockException(string itemName, int quantityRequested, int quantityAvailable)
            : base($"Cannot issue {quantityRequested} of \"{itemName}\" because only {quantityAvailable} are on hand.")
        {
            ItemName = itemName;
            QuantityRequested = quantityRequested;
            QuantityAvailable = quantityAvailable;
        }
    }
}
