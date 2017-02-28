using System.Windows;
using System.Windows.Media;
using Electricals_PointOfSale.Views;
using System.Windows.Controls;
using System.Collections.Generic;
using Electricals_PointOfSale.Models;
using System;

namespace Electricals_PointOfSale

{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        ControlsCleaner clearControls = new ControlsCleaner();
        DataBaseHandler database = new DataBaseHandler();
        Printer printMan = new Printer();
        

        private string newPrinterName = string.Empty;
        public Checkout()

        {
      
            InitializeComponent();
        }
        private double totalDue;
        private double balanceDue;
        private double paidAmount;
        private int activeCounter = 0;
      

        private List<string> names;
        private List<string> prices;
        private List<string> quantities;


    
        public double gsTotal
        {
            get { return totalDue; }
            set { totalDue = value; }
        }
        public List<string> gsNames
        {
            get { return names; }
            set { names = value; }
        }
        public List<string> gsPrices
        {
            get { return prices; }
            set { prices = value; }
        }
        public List<string> gsQuantities
        {
            get { return quantities; }
            set { quantities = value; }
        }


        private void clearFields()
        {
            TextBox[] myTxtBox = { txtbxAmountPaid };
            TextBlock[] myTxtBlocks = { txtblkChangeDue };
            clearControls.clearTextBoxes(myTxtBox, Brushes.Wheat);
            clearControls.clearTextBlocks(myTxtBlocks, Brushes.Wheat);
        }
        private void txtbxAmountPaid_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            double amountPaid;
            double.TryParse(txtbxAmountPaid.Text, out amountPaid);
            txtblkChangeDue.Text = (amountPaid - totalDue).ToString();
            double changeDue;
            double.TryParse(txtblkChangeDue.Text, out changeDue);
            if (changeDue < 0)
            {
                txtblkChangeDue.Foreground = Brushes.Red;
            }
            else
                txtblkChangeDue.Foreground = Brushes.Black;
            paidAmount = amountPaid;
            balanceDue = changeDue;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
           
            clearFields();
           this.Hide();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            activeCounter = 0;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            //if(activeCounter > 0)
            //{
                // updateSales();
               // MessageBox.Show(activeCounter.ToString());
               // activeCounter = 0;
            //}
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            activeCounter = activeCounter + 1;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            txtblkChangeDue.Text = (0 - totalDue).ToString();
        }


        private void btnPrintReceipt_Click(object sender, RoutedEventArgs e)
        {
            if (balanceDue >= 0)
            {
                printMan.itemNames = names.ToArray();
                printMan.itemPrices = prices.ToArray();
                printMan.itemQtys = quantities.ToArray();

                printMan.gsTotal = totalDue;
                printMan.gsBalance = balanceDue;
                printMan.gsPaid = paidAmount;

                if (StockChecker.getInstance().checkStock(names.ToArray()).Length < 1)
                {
                    printMan.callPrintReceipt();

                    if (PrintSuccess.getInstance().gsIsPrintSuccessfull == true)
                    {

                        SalesUpdate.getInstance().gsSoldItemNames = names.ToArray();
                        SalesUpdate.getInstance().gsSoldItemPrices = prices.ToArray();
                        SalesUpdate.getInstance().gsSoldItemQuantities = quantities.ToArray();

                        SalesUpdate.getInstance().updateSales();

                        names.Clear();
                        quantities.Clear();
                        prices.Clear();


                        //new Checkout().ShowDialog();
                        //MainWindow.getInstance().Activate();
                        //this.Close();
                    }
                }else
                {
                    MessageBox.Show("The CheckOut failed Due to Insuffecient Stock For The Following Items: " +
                        " " + string.Join("," , StockChecker.getInstance().checkStock(names.ToArray()) )+ "  Please Restock " +
                        " And Retry", "Insuffecient Stock",MessageBoxButton.OK);
                    names.Clear();
                    quantities.Clear();
                    prices.Clear();
                }
              
            }
            else
                MessageBox.Show("The Amount Paid is Insuffecient. Please top up to avoid loss", "Warning: Insuffecient Funds", MessageBoxButton.OK, MessageBoxImage.Warning);
                       
        }

     
    }
}
