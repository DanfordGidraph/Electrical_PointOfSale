using Electricals_PointOfSale;

namespace Electricals_PointOfSale.Models
{
    class StockUpdater
    {
        private static StockUpdater Instance = null;
        DataBaseHandler database = new DataBaseHandler();

        private StockUpdater() { }

        public static StockUpdater getInstance()
        {
            if (Instance == null)
            {
                Instance = new StockUpdater();
                return Instance;
            } else
                return Instance;
        }


        public void updateStock(string itemName, string quantitySold)
        {
            database.selectQuery("SELECT ProductTable FROM ItemsTableNames WHERE ProductName = '" + itemName +"';");
            string itemTable = database.sqlDataTable.Rows[0]["ProductTable"].ToString();

            double itemSoldQty;
            double.TryParse(quantitySold, out itemSoldQty);

            database.updateQuery("UPDATE " + itemTable + " SET SoldStock = SoldStock + " + itemSoldQty + " WHERE Name = '"+ itemName +"';");
            database.updateQuery("UPDATE " + itemTable + " SET RemainingStock = StartingStock - SoldStock WHERE Name = '" + itemName + "';");

        }

    }
}
