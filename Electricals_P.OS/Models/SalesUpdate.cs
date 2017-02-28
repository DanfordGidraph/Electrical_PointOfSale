using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Electricals_PointOfSale.Models
{
    class SalesUpdate
    {
        DataBaseHandler database = new DataBaseHandler();
        //private properties
        private static SalesUpdate Instance = null;
        private string[] soldItemNames;
        private string[] soldItemQuantities;
        private string[] soldItemPrices;

        //public properties
        public string[] gsSoldItemNames
        {
            get { return soldItemNames; }
            set { soldItemNames = value; }
        }
        public string[] gsSoldItemQuantities
        {
            get { return soldItemQuantities; }
            set { soldItemQuantities = value; }
        }
        public string[] gsSoldItemPrices
        {
            get { return soldItemPrices; }
            set { soldItemPrices = value; }
        }


        private SalesUpdate() { }

        public static SalesUpdate getInstance()
        {
            if (Instance == null)
            {
                return Instance = new SalesUpdate();
            }
            else
                return Instance;
        }

        public void updateSales()
        {
            string dateToday = DateTime.Today.ToShortDateString();
            string convertedDateToday = dateToday.Replace('/', '_');
            //MessageBox.Show(convertedDateToday);
            
            for (int i = 0; i < soldItemNames.Length; i++)
            {
                database.selectQuery("SELECT ProductQty,AvgPrice,TotalDebits FROM Sales_" + convertedDateToday + " WHERE ProductName = '" + soldItemNames[i] + "';");
                if(database.numRows > 0)
                {
                    int currentProductQty;
                    double currentProductAvgPrice;
                    double currentTotalDebits;

                    int updatedProductQty;
                    double updatedProductAvgPrice;
                    double updatedTotalDebits;

                    int.TryParse(database.sqlDataTable.Rows[0]["ProductQty"].ToString(), out currentProductQty);
                    double.TryParse(database.sqlDataTable.Rows[0]["AvgPrice"].ToString(), out currentProductAvgPrice);
                    double.TryParse(database.sqlDataTable.Rows[0]["TotalDebits"].ToString(), out currentTotalDebits);

                    updatedProductQty = currentProductQty + int.Parse(soldItemQuantities[i]);
                    updatedProductAvgPrice = ((double.Parse(soldItemPrices[i]) / double.Parse(soldItemQuantities[i])) + (currentProductAvgPrice)) / 2;
                    updatedTotalDebits = currentTotalDebits + double.Parse(soldItemPrices[i]);

                    database.updateQuery("UPDATE Sales_" + convertedDateToday + " SET ProductQty = '"+ updatedProductQty +"', "+
                        "AvgPrice = '"+ updatedProductAvgPrice +"', TotalDebits = '"+ updatedTotalDebits +"';");
                }
                else
                {
                    double itemPrice;
                    double itemQty;
                    double itemAvgPrice;

                    double.TryParse(soldItemPrices[i], out itemPrice);
                    double.TryParse(soldItemQuantities[i], out itemQty);
                    itemAvgPrice = itemPrice / itemQty;


                    object[] parameters = {
                        soldItemNames[i],
                        itemQty,
                        itemAvgPrice,
                        itemPrice
                    };
                    database.insertQuery("INSERT INTO Sales_" + convertedDateToday + "(ProductName,ProductQty,AvgPrice,TotalDebits)" +
                        " VALUES(@p0,@p1,@p2,@p3)", parameters);
                }
            }

            PrintSuccess.getInstance().gsIsPrintSuccessfull = false;
        }
    }
}
