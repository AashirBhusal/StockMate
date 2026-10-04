using System;
using System.IO;
using System.Windows.Forms;
using StockMate.Data;
using StockMate.Forms;

namespace StockMate
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                // Keep the database next to the program itself. A plain file
                // name is looked up in whatever folder the program was started
                // from, so a shortcut and "dotnet run" could each end up with
                // a different, empty database.
                string databaseFile = Path.Combine(AppContext.BaseDirectory, "stockmate.db");
                Database database = new Database(databaseFile);
                database.CreateTables();
                Application.Run(new MainForm(database));
            }
            catch (Exception ex)
            {
                MessageBox.Show("StockMate could not start: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
