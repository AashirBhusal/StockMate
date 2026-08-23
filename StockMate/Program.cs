using System;
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
                Database database = new Database("stockmate.db");
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
