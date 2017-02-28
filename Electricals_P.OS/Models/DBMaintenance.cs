using System.IO;
using System.Windows;

namespace Electricals_PointOfSale.Models
{

    class DBMaintenance
    {
        DataBaseHandler database = new DataBaseHandler();

        private static DBMaintenance Instance;

        private DBMaintenance() { }

        public static DBMaintenance getInsance()
        {
            if (Instance == null)
            {
                return Instance = new DBMaintenance();
            }
            else
                return Instance;
        }

        public void checkFileExist()
        {
            database.selectQuery("SELECT ProductName,ProductTable FROM ItemsTableNames");
            string[] pNames = new string[database.numRows];
            string[] pTables = new string[database.numRows];

            int rowCount = database.numRows;
            if (rowCount > 0)
            {
                for (int i = 0; i < database.numRows; i++)
                {
                    pNames[i] = database.sqlDataTable.Rows[i]["ProductName"].ToString();
                    pTables[i] = database.sqlDataTable.Rows[i]["ProductTable"].ToString();
                }

                for (int j = 0; j < rowCount; j++)
                {
                    //MessageBox.Show(pNames[j] + "  ---  " + pTables[j]);
                    database.selectQuery("SELECT PicturePath FROM " + pTables[j] + " WHERE Name = '" + pNames[j] + "' ");
                    if (database.sqlDataTable.Rows.Count > 0)
                    {
                        if (!File.Exists(database.sqlDataTable.Rows[0].ToString()))
                        {
                            database.deleteQuery("DELETE FROM " + pTables[j] + " WHERE Name = '" + pNames[j] + "'");
                        }
                    }
                    else
                        database.deleteQuery("DELETE FROM ItemsTableNames WHERE ProductName = '" + pNames[j] + "'");
                   
                }
            }
        }
    }
}
