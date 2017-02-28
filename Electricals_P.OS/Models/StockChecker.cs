using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricals_PointOfSale.Models
{
    class StockChecker
    {
        DataBaseHandler database = new DataBaseHandler();
        private static StockChecker Instance;

        public static StockChecker getInstance()
        {
            if (Instance == null)
            {
                return Instance = new StockChecker();
            }
            else
                return Instance;
        }

        private StockChecker() { }

        public string[] checkStock(string[] itemNames)
        {
            List<string> underStockedItems = new List<string>(itemNames.Length);
            foreach (string item in itemNames)
            {
                string itemTableName;
                database.selectQuery("SELECT ProductTable FROM ItemsTableNames WHERE ProductName = '" + item + "';");
                itemTableName = database.sqlDataTable.Rows[0][0].ToString();
                database.selectQuery("SELECT RemainingStock FROM " + itemTableName + " WHERE Name = '"+ item +"';");
                int remStock;
                int.TryParse(database.sqlDataTable.Rows[0]["RemainingStock"].ToString(), out remStock);


                if (remStock < 0)
                {
                    underStockedItems.Add(item);
                }
            }
            return underStockedItems.ToArray();
        }
    }
}
