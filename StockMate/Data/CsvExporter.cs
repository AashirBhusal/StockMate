using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using StockMate.Models;

namespace StockMate.Data
{
    // Writes a stock list to a CSV file that Excel can open.
    // It works on a List<StockItem>, so it never needs to know whether a row
    // is a consumable or a durable. The Status column comes from the same
    // NeedsAttention() and AttentionMessage() calls the main window uses.
    public class CsvExporter
    {
        // Writes the file and returns the total value of everything in it,
        // so the form can show that figure to the user.
        public decimal Export(List<StockItem> items, string fileName)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Item,Category,Type,Quantity,Unit,Reorder level,Unit cost,Total value,Status");

            decimal totalValue = 0;

            foreach (StockItem item in items)
            {
                string status = item.NeedsAttention() ? item.AttentionMessage() : "OK";

                csv.AppendLine(
                    Escape(item.Name) + "," +
                    Escape(item.Category) + "," +
                    item.ItemType + "," +
                    item.QuantityOnHand + "," +
                    Escape(item.Unit) + "," +
                    item.ReorderLevel + "," +
                    Money(item.UnitCost) + "," +
                    Money(item.TotalValue) + "," +
                    Escape(status));

                totalValue = totalValue + item.TotalValue;
            }

            csv.AppendLine(",,,,,,Total stock value," + Money(totalValue) + ",");

            // The byte order mark at the start tells Excel the file is UTF-8.
            File.WriteAllText(fileName, csv.ToString(), new UTF8Encoding(true));
            return totalValue;
        }

        // Always a full stop for the decimal point. Some Windows regions use a
        // comma instead, which would split one number across two columns.
        private static string Money(decimal amount)
        {
            return amount.ToString("0.00", CultureInfo.InvariantCulture);
        }

        // A value containing a comma, a quote mark or a line break has to be
        // wrapped in quotes, with any quote marks inside it doubled.
        private static string Escape(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
                return "\"" + value.Replace("\"", "\"\"") + "\"";

            return value;
        }
    }
}
