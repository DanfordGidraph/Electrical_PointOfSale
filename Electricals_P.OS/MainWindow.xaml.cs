using Electricals_PointOfSale.Models;
using Electricals_PointOfSale.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ItemsAdding _ItemAdder = new ItemsAdding();
        InventoryEditor _InventoryEditor = new InventoryEditor();
        DataBaseHandler database = new DataBaseHandler();
        ListBoxFiller lbFiller = new ListBoxFiller();
        Checkout _Checkout = new Checkout();
        ControlsCleaner cleaner = new ControlsCleaner();

        private static MainWindow Instance; 
        private int imageNum = 1;
        private double itemMinPrice;
        string iTemTableName;
        private double stockRemaining;
        private string[] itemPriceArray;
     
       

        private List<string> receiptItemsNames = new List<string>(13);
        private List<string> receiptItemsQty = new List<string>(13);
        private List<string> receiptItemsPrice = new List<string>(13);
      

        public MainWindow()
        {
            InitializeComponent();
        }
        public static MainWindow getInstance()
        {
            if (Instance == null)
            {
                return Instance = new MainWindow();
            }
            else
                return Instance;
        }
     
        private void clearFields(ref int imgNum)
        {
            TextBlock[] myTextBlocks = {txtblkReceiptItemName1, txtblkReceiptItemName2, txtblkReceiptItemName3, txtblkReceiptItemName4,
                                        txtblkReceiptItemName5,txtblkReceiptItemName6,txtblkReceiptItemName7,txtblkReceiptItemName8,
                                        txtblkReceiptItemName9,txtblkReceiptItemName10,txtblkReceiptItemName11,txtblkReceiptItemName12,
                                        txtblkReceiptItemName13,

                                        txtblkReceiptItemPrice1,txtblkReceiptItemPrice2,txtblkReceiptItemPrice3,txtblkReceiptItemPrice4,
                                        txtblkReceiptItemPrice5,txtblkReceiptItemPrice6,txtblkReceiptItemPrice7,txtblkReceiptItemPrice8,
                                        txtblkReceiptItemPrice9,txtblkReceiptItemPrice10,txtblkReceiptItemPrice11,txtblkReceiptItemPrice12,
                                        txtblkReceiptItemPrice13,

                                        txtblkReceiptItemQty1,txtblkReceiptItemQty2,txtblkReceiptItemQty3,txtblkReceiptItemQty4,txtblkReceiptItemQty5,
                                        txtblkReceiptItemQty6,txtblkReceiptItemQty7,txtblkReceiptItemQty8,txtblkReceiptItemQty9,txtblkReceiptItemQty10,
                                        txtblkReceiptItemQty11,txtblkReceiptItemQty12,txtblkReceiptItemQty13,

                                        };
            Image[] myImages = {imgCartItem1, imgCartItem2 , imgCartItem3 , imgCartItem4 , imgCartItem5 , imgCartItem6 , imgCartItem7
            ,imgCartItem8,imgCartItem9,imgCartItem10,imgCartItem11,imgCartItem12,imgCartItem13,imgCartItem14,imgCartItem15,imgCartItem16};

            txtblkReceiptTotalPrice.Text = "0";
            cleaner.clearTextBlocks(myTextBlocks, Brushes.Wheat);
            cleaner.clearImages(myImages);

            receiptItemsNames.Clear();
            receiptItemsPrice.Clear();
            receiptItemsQty.Clear();
            imgNum = 1;
        }
  
        private void btnAddNewItemToInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserAccessLevel.getInstance().gsCurrentUserAccessLevel == "Admin")
                {
                    _ItemAdder.ShowDialog();
                }
                else {
                    MessageBoxResult result;
                   result = MessageBox.Show("You are NOT logged in as an Administrator and Therefore Have No Permission To Add " +
                            " Items to The Inventory. Click OK To Retry Login or Cancel To Quit", "Access Denied",
                            MessageBoxButton.OKCancel, MessageBoxImage.Hand);

                    if(result == MessageBoxResult.OK)
                    {
                        new LoginWindow().ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEditItemInInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _InventoryEditor.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnProceedeToCheckout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserAccessLevel.getInstance().gsCurrentUserAccessLevel != string.Empty)
                {
                    double total;
                    double.TryParse(txtblkReceiptTotalPrice.Text, out total);
                    _Checkout.gsTotal = total;
                    _Checkout.gsNames = receiptItemsNames;
                    _Checkout.gsPrices = receiptItemsPrice;
                    _Checkout.gsQuantities = receiptItemsQty;
                    _Checkout.ShowDialog();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("You are NOT logged in as an Administrator and Therefore Have No Permission To Sell " +
                            " Items. Click OK To Retry Login or Cancel To Quit", "Access Denied",
                            MessageBoxButton.OKCancel, MessageBoxImage.Hand);
                    if (result == MessageBoxResult.OK)
                    {
                        new LoginWindow().ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
           
        }

        private void MainWindowUI_Activated(object sender, EventArgs e)
        {
            clearFields(ref imageNum);
            txtbxSellPrice.Text = "0";
            txtblkQuantity.Text = "1";
        }
        private void MainWindowUI_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(UserAccessLevel.getInstance().gsCurrentUserName);
            DBMaintenance.getInsance().checkFileExist();
            database.selectQuery("SELECT RunningDate FROM DateKeeper");
            if(database.sqlDataTable.Rows.Count == 0)
            {
                string originalDate = DateTime.Today.ToShortDateString();
                string editedDate = originalDate.Replace('/', '_');
                object[] paramArray = {editedDate};
                database.insertQuery("INSERT INTO DateKeeper(RunningDate) VALUES(@p0)",paramArray);
            }
            else 
            {
                string dateToday = DateTime.Today.ToShortDateString();
                string convertedDateToday = dateToday.Replace('/', '_');

                string dateInDb = database.sqlDataTable.Rows[0][0].ToString();

                if (convertedDateToday != dateInDb)
                {
                    database.updateQuery("UPDATE DateKeeper SET RunningDate = '" + convertedDateToday + "';");
                    database.createTable("CREATE TABLE `Sales_" + convertedDateToday + "` (" +
                                           " `ProductNum`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                                           " `ProductName`	TEXT NOT NULL," +
                                           " `ProductQty`   NUMERIC NOT NULL DEFAULT 0,"+
                                           " `AvgPrice`   NUMERIC NOT NULL DEFAULT 0," +
                                           " `TotalDebits`	NUMERIC NOT NULL DEFAULT 0" +
                                           " ); ");
                }else
                    database.createTable("CREATE TABLE IF NOT EXISTS `Sales_" + convertedDateToday + "` (" +
                                           " `ProductNum`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                                           " `ProductName`	TEXT NOT NULL," +
                                           " `ProductQty`   NUMERIC NOT NULL DEFAULT 0," +
                                           " `AvgPrice`   NUMERIC NOT NULL DEFAULT 0," +
                                           " `TotalDebits`	NUMERIC NOT NULL DEFAULT 0" +
                                           " ); ");
            }
           
        }
        private void MainWindowUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }


        private void styleButtons(Button btn)
        {
            ButtonsStyle btnStyler = new ButtonsStyle();
            btnStyler.styleMainWindowButtons(new Button[] {btnCategoriesAdhesives,btnCategoriesChandeliers,btnCategoriesExtensions,btnCategoriesFittings,
          btnCategoriesJacks,btnCategoriesLamps,btnCategoriesLights,btnCategoriesMiscellaneous,btnCategoriesPipes,btnCategoriesPowerCords,
          btnCategoriesSconces,btnCategoriesSockets,btnCategoriesSwitches,btnCategoriesUtilities}, btn);
        }
        private void handleCategoryClicks(Button btn, string tableName)
        {
            try
            {
                iTemTableName = tableName;
                styleButtons(btn);
                lstbxItemsInCategory.Items.Clear();
                ListBoxItem notSelected = new ListBoxItem();

                notSelected.Content = "Select An Item".PadLeft(5);
                lstbxItemsInCategory.Items.Add(notSelected);
                lstbxItemsInCategory.SelectedIndex = 0;
                database.selectQuery("SELECT PicturePath FROM " + tableName + ";");

                for (int j = 0; j < database.numRows; j++)
                {
                    string picPath = database.sqlDataTable.Rows[j]["PicturePath"].ToString();
                    if (!File.Exists(picPath))
                    {
                        database.deleteQuery("DELETE FROM " + tableName + " WHERE PicturePath = '" + picPath + "';");
                    }
                }
                database.selectQuery("SELECT Name,MinPrice FROM " + tableName + ";");
                string[] lbContent = new string[database.numRows];
                string[] itemPrice = new string[database.numRows];
                for (int i = 0; i < database.numRows; i++)
                {
                    string contentStr = "->" + database.sqlDataTable.Rows[i]["Name"].ToString().PadRight(25, '.') + database.sqlDataTable.Rows[i]["MinPrice"].ToString().PadLeft(6, '.');
                    lbContent[i] = contentStr;
                    itemPrice[i] = database.sqlDataTable.Rows[i]["MinPrice"].ToString();
                  
                }
                itemPriceArray = itemPrice;
                lbFiller.fillListBox(lbContent, lstbxItemsInCategory);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
           
        }

        private void btnCategoriesLights_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesLights, "Lights");
        }
        private void btnCategoriesSwitches_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSwitches, "Switches");

        }
        private void btnCategoriesSockets_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSockets, "Sockets");

        }
        private void btnCategoriesPowerCords_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesPowerCords, "PowerCords");

        }
        private void btnCategoriesLamps_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesLamps, "Lamps");

        }
        private void btnCategoriesJacks_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesJacks, "Jacks");

        }
        private void btnCategoriesMiscellaneous_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesMiscellaneous, "Miscellaneous");

        }
        private void btnCategoriesPipes_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesPipes, "Pipes");
        }
        private void btnCategoriesUtilities_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesUtilities, "Utilities");
        }
        private void btnCategoriesSconces_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSconces, "Sconces");
        }
        private void btnCategoriesFittings_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesFittings, "Fittings");
        }
        private void btnCategoriesAdhesives_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesAdhesives, "Adhesives");
        }
        private void btnCategoriesExtensions_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesExtensions, "Extensions");
        }
        private void btnCategoriesChandeliers_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesChandeliers, "Chandeliers");
        }

        private void lstbxItemsInCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstbxItemsInCategory.SelectedIndex > 0)
            {
                try
                {
                    string name = ((ListBoxItem)lstbxItemsInCategory.SelectedValue).Content.ToString();
                    char[] chars = { '-', '>', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                    string nameToDisplay = name.Trim(chars);

                    txtblkSelectedItem.Text = nameToDisplay;
                    txtblkQuantity.Text = "1";

                    database.selectQuery("SELECT RemainingStock FROM " + iTemTableName + " WHERE Name = '" + nameToDisplay + "';");
                    double remainingStock;
                    double.TryParse(database.sqlDataTable.Rows[0]["RemainingStock"].ToString(), out remainingStock);
                    stockRemaining = remainingStock;
                    txtbxSellPrice.Text = itemPriceArray[((lstbxItemsInCategory.SelectedIndex) - 1)];
                    double.TryParse(txtbxSellPrice.Text, out itemMinPrice);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               
            }
            else if (lstbxItemsInCategory.SelectedIndex == 0)
            {
                txtblkSelectedItem.Text = string.Empty;

            }
        }

        private void btnAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            int oldQty;
            int.TryParse(txtblkQuantity.Text, out oldQty);
            txtblkQuantity.Text = (oldQty + 1).ToString();
        }
        private void btnSubtractQuantity_Click(object sender, RoutedEventArgs e)
        {
            int oldQty;
            int.TryParse(txtblkQuantity.Text, out oldQty);
            txtblkQuantity.Text = (oldQty - 1).ToString();
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (lstbxItemsInCategory.SelectedIndex > 0)
            {
                int itemQty;
                int.TryParse(txtblkQuantity.Text, out itemQty);
                if (findStockRemaining() > itemQty)
                {
                    try
                    {
                        string itemQuantity = txtblkQuantity.Text;


                        string itemName = txtblkSelectedItem.Text;
                        database.selectQuery("SELECT PicturePath FROM " + iTemTableName + " WHERE Name = '" + itemName + "';");
                        string picPath = database.sqlDataTable.Rows[0][0].ToString();
                        Uri picUri = new Uri(picPath);

                        double itemPrice;
                        double.TryParse(txtbxSellPrice.Text, out itemPrice);
                        handleAddToCart(picUri, itemName, itemQuantity, itemPrice);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Add This Item With The Specified Quantity! There are Only "+findStockRemaining()+" Left"+
                        "Please Revise the Order or Update the Inventory To Retry ","Insuffecient Stock",MessageBoxButton.OK,MessageBoxImage.Information);
                }
               

               
            }
        }
        private void handleAddToCart(Uri imgSource, string Name, string Qty, double Price)
        {
            double priceInTxtBx;
            double.TryParse(txtbxSellPrice.Text, out priceInTxtBx);

            try
            {
                if (imageNum < 14)
                {
                    if (priceInTxtBx >= itemMinPrice)

                    {
                        showImageInCart(imageNum).Source = new BitmapImage(imgSource);
                        showImageInCart(imageNum).Stretch = Stretch.Fill;
                        showItemName(imageNum).Text = Name;
                        showItemQty(imageNum).Text = Qty;

                        double quantity;
                        double.TryParse(Qty, out quantity);
                        Price = quantity * Price;

                        showItemPrice(imageNum).Text = Price.ToString();

                        double total;
                        double.TryParse(txtblkReceiptTotalPrice.Text, out total);
                        txtblkReceiptTotalPrice.Text = (total + Price).ToString();

                        receiptItemsNames.Insert((imageNum - 1), Name);
                        receiptItemsQty.Insert((imageNum - 1), Qty);
                        receiptItemsPrice.Insert((imageNum - 1), Price.ToString());

                        imageNum = imageNum + 1;

                    }
                    else MessageBox.Show("The Selling Price Entered is below the Minimum Price For The Item. Please Revise to Avoid Loss", "Warning: Iminent Loss", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else MessageBox.Show("The Receipt Is Filled. To Proceede please start a new Sale", "Full Receipt", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            
        }
        private Image showImageInCart(int index)
        {
            if (index == 1)
            {
                
                          return imgCartItem1;

            }
            else if (index == 2)
            {
              
                return imgCartItem2;
            }
            else if (index == 3)
            {
                
                return imgCartItem3;
            }
            else if (index == 4)
            {
              
                return imgCartItem4;
            }
            else if (index == 5)
            {
               
                return imgCartItem5;
            }
            else if (index == 6)
            {
             
                return imgCartItem6;
            }
            else if (index == 7)
            {
               
                return imgCartItem7;
            }
            else if (index == 8)
            {
               
                return imgCartItem8;
            }
            else if (index == 9)
            {
               
                return imgCartItem9;
            }
            else if (index == 10)
            {
               
                return imgCartItem10;
            }
            else if (index == 11)
            {
                return imgCartItem11;
            }
            else if (index == 12)
            {
                return imgCartItem12;
            }
            else if (index == 13)
            {
                return imgCartItem13;
            }
            else if (index == 14)
            {
                return imgCartItem14;
            }
            else if (index == 15)
            {
                return imgCartItem15;
            }
            else if (index == 16)
            {
                return imgCartItem16;
            }
            return null;
        }
        private TextBlock showItemName(int index)
        {
            if (index == 1)
            {
                return txtblkReceiptItemName1;
            }else if(index == 2)
            {
                return txtblkReceiptItemName2;
            }
            else if (index == 3)
            {
                return txtblkReceiptItemName3;
            }
            else if (index == 4)
            {
                return txtblkReceiptItemName4;
            }
            else if (index == 5)
            {
                return txtblkReceiptItemName5;
            }
            else if (index == 6)
            {
                return txtblkReceiptItemName6;
            }
            else if (index == 7)
            {
                return txtblkReceiptItemName7;
            }
            else if (index == 8)
            {
                return txtblkReceiptItemName8;
            }
            else if (index == 9)
            {
                return txtblkReceiptItemName9;
            }
            else if (index == 10)
            {
                return txtblkReceiptItemName10;
            }
            else if (index == 11)
            {
                return txtblkReceiptItemName11;
            }
            else if (index == 12)
            {
                return txtblkReceiptItemName12;
            }
            else if (index == 13)
            {
                return txtblkReceiptItemName13;
            }
            else
            return null;
        }
        private TextBlock showItemPrice(int index)
        {
            if (index == 1)
            {
                return txtblkReceiptItemPrice1;
            }
            else if (index == 2)
            {
                return txtblkReceiptItemPrice2;
            }
            else if (index == 3)
            {
                return txtblkReceiptItemPrice3;
            }
            else if (index == 4)
            {
                return txtblkReceiptItemPrice4;
            }
            else if (index == 5)
            {
                return txtblkReceiptItemPrice5;
            }
            else if (index == 6)
            {
                return txtblkReceiptItemPrice6;
            }
            else if (index == 7)
            {
                return txtblkReceiptItemPrice7;
            }
            else if (index == 8)
            {
                return txtblkReceiptItemPrice8;
            }
            else if (index == 9)
            {
                return txtblkReceiptItemPrice9;
            }
            else if (index == 10)
            {
                return txtblkReceiptItemPrice10;
            }
            else if (index == 11)
            {
                return txtblkReceiptItemPrice11;
            }
            else if (index == 12)
            {
                return txtblkReceiptItemPrice12;
            }
            else if (index == 13)
            {
                return txtblkReceiptItemPrice13;
            }
            else
                return null;
        }
        private TextBlock showItemQty(int index)
        {
            if (index == 1)
            {
                return txtblkReceiptItemQty1;
            }
            else if (index == 2)
            {
                return txtblkReceiptItemQty2;
            }
            else if (index == 3)
            {
                return txtblkReceiptItemQty3;
            }
            else if (index == 4)
            {
                return txtblkReceiptItemQty4;
            }
            else if (index == 5)
            {
                return txtblkReceiptItemQty5;
            }
            else if (index == 6)
            {
                return txtblkReceiptItemQty6;
            }
            else if (index == 7)
            {
                return txtblkReceiptItemQty7;
            }
            else if (index == 8)
            {
                return txtblkReceiptItemQty8;
            }
            else if (index == 9)
            {
                return txtblkReceiptItemQty9;
            }
            else if (index == 10)
            {
                return txtblkReceiptItemQty10;
            }
            else if (index == 11)
            {
                return txtblkReceiptItemQty11;
            }
            else if (index == 12)
            {
                return txtblkReceiptItemQty12;
            }
            else if (index == 13)
            {
                return txtblkReceiptItemQty13;
            }
            else
                return null;
        }
        
        private int findStockRemaining()
        {
          
                string itemName = txtblkSelectedItem.Text;
                database.selectQuery("SELECT RemainingStock FROM " + iTemTableName + " WHERE Name = '" + itemName + "';");
                int remStock;
                int.TryParse(database.sqlDataTable.Rows[0]["RemainingStock"].ToString(), out remStock);
                return remStock;
           
        }



        private void handleReceiptItemCancel(int itmNum)
        {
            try
            {
                if (showItemName(itmNum).Background == Brushes.Wheat)
                {
                    if (showImageInCart(itmNum).Source != null)
                    {

                        Uri canceldItemImaageSource = new Uri(Path.Combine(Environment.CurrentDirectory, @"Images\Miscellaneous\itemCanceledOverlayImage.png"));
                        showImageInCart(itmNum).Source = new BitmapImage(canceldItemImaageSource);
                        showImageInCart(itmNum).Stretch = System.Windows.Media.Stretch.Uniform;
                        showImageInCart(itmNum).Opacity = .8;

                        double total;
                        double.TryParse(txtblkReceiptTotalPrice.Text, out total);
                        txtblkReceiptTotalPrice.Text = (total - double.Parse(showItemPrice(itmNum).Text)).ToString();

                        receiptItemsNames.RemoveAt(itmNum - 1);
                        receiptItemsPrice.RemoveAt(itmNum - 1);
                        receiptItemsQty.RemoveAt(itmNum - 1);

                        showItemName(itmNum).Text = string.Empty;
                        showItemPrice(itmNum).Text = string.Empty;
                        showItemQty(itmNum).Text = string.Empty;

                        showItemName(itmNum).Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"Images/Backgrounds/redSquiglyLine.png"))));
                        showItemPrice(itmNum).Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"Images/Backgrounds/redSquiglyLine.png"))));
                        showItemQty(itmNum).Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"Images/Backgrounds/redSquiglyLine.png"))));

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void btnCancelReceiptItem1_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(1);
        }
        private void btnCancelReceiptItem2_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(2);
        }
        private void btnCancelReceiptItem3_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(3);
        }
        private void btnCancelReceiptItem4_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(4);
        }
        private void btnCancelReceiptItem5_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(5);
        }
        private void btnCancelReceiptItem6_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(6);
        }
        private void btnCancelReceiptItem7_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(7);
        }
        private void btnCancelReceiptItem8_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(8);
        }
        private void btnCancelReceiptItem9_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(9);
        }
        private void btnCancelReceiptItem10_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(10);
        }
        private void btnCancelReceiptItem11_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(11);
        }
        private void btnCancelReceiptItem12_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(12);
        }
        private void btnCancelReceiptItem13_Click(object sender, RoutedEventArgs e)
        {
            handleReceiptItemCancel(13);
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            clearFields(ref imageNum);
            txtblkQuantity.Text = "1";
        }

        private void btnShowStatistics_Click(object sender, RoutedEventArgs e)
        {
            new StatisticsWindow().ShowDialog();
        }

        private void btnExportData_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow().ShowDialog();
        }
    }
}
