using Electricals_PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        DataBaseHandler database = new DataBaseHandler();

        //private static StatisticsWindow Instance = null;


        //public static StatisticsWindow getInstance()
        //{
        //    if (Instance == null)
        //    {
        //        return Instance = new StatisticsWindow();

        //    }
        //    else
        //        return Instance;
        //}
        public StatisticsWindow()
        {
            InitializeComponent();
        }

        private void StatisticsWindowUI_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbItem2 = new ComboBoxItem();
            cbItem2.Content = "Select A Category";
            cmboxSortByCategory.Items.Add(cbItem2);
        }

        private void fillComboWithDays()
        {

        }

              private void cmboxSortByDay_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Parse(dtpSortByDay.ToString());
            
          
            string convertedDateToday = dt.ToShortDateString().Replace('/', '_');

            database.selectQuery("SELECT ProductName,TotalDebits FROM Sales_" + convertedDateToday + ";");
            //MessageBox.Show(convertedDateToday);
            if (database.numRows > 0)
            {

                string[] itemNames = new string[database.numRows];
                double[] itemSoldTotals = new double[database.numRows];

                for (int rowNum = 0; rowNum < database.numRows; rowNum++)
                {
                    itemNames[rowNum] = database.sqlDataTable.Rows[rowNum]["ProductName"].ToString();
                    itemSoldTotals[rowNum] = double.Parse(database.sqlDataTable.Rows[rowNum]["TotalDebits"].ToString());

                    //MessageBox.Show(itemNames[rowNum]+"   " + itemSoldTotals[rowNum].ToString());

                }

                KeyValuePair<string, double>[] kvpArray = new KeyValuePair<string, double>[itemSoldTotals.Length];
                for (int i = 0; i < itemSoldTotals.Length; i++)
                {
                    if (itemSoldTotals[i] == 0)
                    {
                        itemNames[i] = string.Empty;
                    }
                    kvpArray[i] = new KeyValuePair<string, double>(itemNames[i], itemSoldTotals[i]);
                }

                ((ColumnSeries)SalesChart.Series[0]).ItemsSource = kvpArray;
            }
            else
            {
                ((ColumnSeries)SalesChart.Series[0]).ItemsSource = null;
            }
        }

    }
}
