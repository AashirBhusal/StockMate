using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using StockMate.Models;

namespace StockMate.Data
{
    // All the SQLite code is in here so the forms do not contain any SQL.
    public class Database
    {
        private readonly string _connectionString;

        public Database(string fileName)
        {
            _connectionString = "Data Source=" + fileName;
        }

        private SqliteConnection Open()
        {
            SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        // Makes the tables the first time the program runs.
        public void CreateTables()
        {
            using (SqliteConnection connection = Open())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS StockItems (" +
                    " ItemId INTEGER PRIMARY KEY AUTOINCREMENT," +
                    " Name TEXT NOT NULL," +
                    " Category TEXT NOT NULL," +
                    " ItemType TEXT NOT NULL," +
                    " Unit TEXT NOT NULL," +
                    " QuantityOnHand INTEGER NOT NULL," +
                    " ReorderLevel INTEGER NOT NULL," +
                    " UnitCost REAL NOT NULL," +
                    " IsActive INTEGER NOT NULL," +
                    " ExpiryDate TEXT," +
                    " ReplacementMonths INTEGER," +
                    " LastReplacedDate TEXT);" +

                    "CREATE TABLE IF NOT EXISTS Transactions (" +
                    " TransactionId INTEGER PRIMARY KEY AUTOINCREMENT," +
                    " ItemId INTEGER NOT NULL," +
                    " TransactionType TEXT NOT NULL," +
                    " Quantity INTEGER NOT NULL," +
                    " TransactionDate TEXT NOT NULL," +
                    " StaffName TEXT NOT NULL," +
                    " FOREIGN KEY (ItemId) REFERENCES StockItems(ItemId));";
                command.ExecuteNonQuery();
            }
        }

        // Reads every active item and builds the right class for each row.
        public List<StockItem> GetAllItems()
        {
            List<StockItem> items = new List<StockItem>();

            using (SqliteConnection connection = Open())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM StockItems WHERE IsActive = 1 ORDER BY Name";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(ReadItem(reader));
                    }
                }
            }

            return items;
        }

        // Turns one database row into a ConsumableItem or a DurableItem.
        // The ItemType column is what tells us which class to build.
        private StockItem ReadItem(SqliteDataReader reader)
        {
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string category = reader.GetString(reader.GetOrdinal("Category"));
            string itemType = reader.GetString(reader.GetOrdinal("ItemType"));
            int quantity = reader.GetInt32(reader.GetOrdinal("QuantityOnHand"));

            StockItem item;

            if (itemType == "Consumable")
            {
                DateTime expiry = DateTime.Parse(reader.GetString(reader.GetOrdinal("ExpiryDate")));
                item = new ConsumableItem(name, category, quantity, expiry);
            }
            else
            {
                int months = reader.GetInt32(reader.GetOrdinal("ReplacementMonths"));
                DateTime lastReplaced = DateTime.Parse(reader.GetString(reader.GetOrdinal("LastReplacedDate")));
                item = new DurableItem(name, category, quantity, months, lastReplaced);
            }

            item.ItemId = reader.GetInt32(reader.GetOrdinal("ItemId"));
            item.Unit = reader.GetString(reader.GetOrdinal("Unit"));
            item.ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel"));
            item.UnitCost = (decimal)reader.GetDouble(reader.GetOrdinal("UnitCost"));
            item.IsActive = reader.GetInt32(reader.GetOrdinal("IsActive")) == 1;

            return item;
        }

        // Adds a new item, then puts the new database id back on the object.
        public void InsertItem(StockItem item)
        {
            using (SqliteConnection connection = Open())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO StockItems (Name, Category, ItemType, Unit, QuantityOnHand," +
                    " ReorderLevel, UnitCost, IsActive, ExpiryDate, ReplacementMonths, LastReplacedDate)" +
                    " VALUES ($name, $category, $type, $unit, $qty, $reorder, $cost, $active," +
                    " $expiry, $months, $replaced);" +
                    " SELECT last_insert_rowid();";

                AddItemParameters(command, item);
                item.ItemId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void UpdateItem(StockItem item)
        {
            using (SqliteConnection connection = Open())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE StockItems SET Name = $name, Category = $category, ItemType = $type," +
                    " Unit = $unit, QuantityOnHand = $qty, ReorderLevel = $reorder, UnitCost = $cost," +
                    " IsActive = $active, ExpiryDate = $expiry, ReplacementMonths = $months," +
                    " LastReplacedDate = $replaced WHERE ItemId = $id";

                AddItemParameters(command, item);
                command.Parameters.AddWithValue("$id", item.ItemId);
                command.ExecuteNonQuery();
            }
        }

        // Named parameters are used instead of joining strings together, so a
        // name containing a quote mark cannot break the query.
        private void AddItemParameters(SqliteCommand command, StockItem item)
        {
            command.Parameters.AddWithValue("$name", item.Name);
            command.Parameters.AddWithValue("$category", item.Category);
            command.Parameters.AddWithValue("$type", item.ItemType);
            command.Parameters.AddWithValue("$unit", item.Unit);
            command.Parameters.AddWithValue("$qty", item.QuantityOnHand);
            command.Parameters.AddWithValue("$reorder", item.ReorderLevel);
            command.Parameters.AddWithValue("$cost", (double)item.UnitCost);
            command.Parameters.AddWithValue("$active", item.IsActive ? 1 : 0);

            ConsumableItem consumable = item as ConsumableItem;
            DurableItem durable = item as DurableItem;

            if (consumable != null)
            {
                command.Parameters.AddWithValue("$expiry", consumable.ExpiryDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("$months", DBNull.Value);
                command.Parameters.AddWithValue("$replaced", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("$expiry", DBNull.Value);
                command.Parameters.AddWithValue("$months", durable.ReplacementMonths);
                command.Parameters.AddWithValue("$replaced", durable.LastReplacedDate.ToString("yyyy-MM-dd"));
            }
        }

        // Saves a movement and the item's new quantity together.
        // Both changes are inside one transaction, so if the program stops
        // half way through, neither of them is saved.
        public void SaveMovement(StockItem item, string transactionType, int quantity, string staffName)
        {
            using (SqliteConnection connection = Open())
            using (SqliteTransaction transaction = connection.BeginTransaction())
            {
                SqliteCommand insert = connection.CreateCommand();
                insert.Transaction = transaction;
                insert.CommandText =
                    "INSERT INTO Transactions (ItemId, TransactionType, Quantity, TransactionDate, StaffName)" +
                    " VALUES ($id, $type, $qty, $date, $staff)";
                insert.Parameters.AddWithValue("$id", item.ItemId);
                insert.Parameters.AddWithValue("$type", transactionType);
                insert.Parameters.AddWithValue("$qty", quantity);
                insert.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                insert.Parameters.AddWithValue("$staff", staffName);
                insert.ExecuteNonQuery();

                SqliteCommand update = connection.CreateCommand();
                update.Transaction = transaction;
                update.CommandText = "UPDATE StockItems SET QuantityOnHand = $qty WHERE ItemId = $id";
                update.Parameters.AddWithValue("$qty", item.QuantityOnHand);
                update.Parameters.AddWithValue("$id", item.ItemId);
                update.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        // The list of categories already used, for the filter box.
        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();

            using (SqliteConnection connection = Open())
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT DISTINCT Category FROM StockItems WHERE IsActive = 1 ORDER BY Category";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(0));
                    }
                }
            }

            return categories;
        }
    }
}
