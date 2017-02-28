using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using Electricals_PointOfSale.Models;
using Electricals_PointOfSale.Views;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for ItemsAdding.xaml
    /// </summary>
    public partial class InventoryEditor : Window

    {
        UploadImage imageUploader = new UploadImage();
        DataBaseHandler database = new DataBaseHandler();
        ListBoxFiller lbFiller = new ListBoxFiller();
        LoginWindow _login = new LoginWindow();
        

        string selected = string.Empty;
        string userStatus;

        string pdName = string.Empty;
        string pdPrice = string.Empty;
        string pdBuyingPrice = string.Empty;
        string pdInStock = string.Empty;
        string pdPic = string.Empty;

        
        public InventoryEditor()
        {
            InitializeComponent();
        }
        private void InventoryEditorUI_Loaded(object sender, RoutedEventArgs e)
        {
            userStatus = UserAccessLevel.getInstance().gsCurrentUserAccessLevel;
        }
        private void InventoryEditorUI_GotFocus(object sender, RoutedEventArgs e)
        {
            userStatus = UserAccessLevel.getInstance().gsCurrentUserAccessLevel;
        }
        private void InventoryEditorUI_Activated(object sender, EventArgs e)
        {
            userStatus = UserAccessLevel.getInstance().gsCurrentUserAccessLevel;
        }


        private void styleButtons(Button btn)
        {
            ButtonsStyle btnStyler = new ButtonsStyle();
            btnStyler.styleAddingButtonsButton(new Button[] {btnCategoriesAdhesives,btnCategoriesChandeliers,btnCategoriesExtensions,btnCategoriesFittings,
          btnCategoriesJacks,btnCategoriesLamps,btnCategoriesLights,btnCategoriesMiscellaneous,btnCategoriesPipes,btnCategoriesPowerCords,
          btnCategoriesSconces,btnCategoriesSockets,btnCategoriesSwitches,btnCategoriesUtilities}, btn);
        }
        private void handleCategoryClicks(Button btn, string tableName, ref string selctedCategory)
        {
            selctedCategory = tableName;
            styleButtons(btn);
            lstbxCategoryItems.Items.Clear();
            ListBoxItem notSelected = new ListBoxItem();

            notSelected.Content = "Select An Item".PadLeft(5);
            lstbxCategoryItems.Items.Add(notSelected);
            lstbxCategoryItems.SelectedIndex = 0;
            database.selectQuery("SELECT Name,MinPrice,RemainingStock,PicturePath FROM " + tableName + ";");

            string[] lbContent = new string[database.numRows];

            for (int i = 0; i < database.numRows; i++)
            {
                lbContent[i] = "-->"+ database.sqlDataTable.Rows[i]["Name"].ToString();
            }

            lbFiller.fillListBox(lbContent, lstbxCategoryItems);
        }

        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================
        private void btnCategoriesLights_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesLights, "Lights", ref selected);
        }
        private void btnCategoriesSwitches_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSwitches, "Switches", ref selected);

        }
        private void btnCategoriesSockets_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSockets, "Sockets", ref selected);

        }
        private void btnCategoriesPowerCords_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesPowerCords, "PowerCords", ref selected);

        }
        private void btnCategoriesLamps_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesLamps, "Lamps", ref selected);

        }
        private void btnCategoriesJacks_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesJacks, "Jacks", ref selected);

        }
        private void btnCategoriesMiscellaneous_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesMiscellaneous, "Miscellaneous", ref selected);

        }
        private void btnCategoriesPipes_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesPipes, "Pipes", ref selected);
        }
        private void btnCategoriesUtilities_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesUtilities, "Utilities", ref selected);
        }
        private void btnCategoriesSconces_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesSconces, "Sconces", ref selected);
        }
        private void btnCategoriesFittings_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesFittings, "Fittings", ref selected);
        }
        private void btnCategoriesAdhesives_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesAdhesives, "Adhesives", ref selected);
        }
        private void btnCategoriesExtensions_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesExtensions, "Extensions", ref selected);
        }
        private void btnCategoriesChandeliers_Click(object sender, RoutedEventArgs e)
        {
            handleCategoryClicks(btnCategoriesChandeliers, "Chandeliers", ref selected);
        }

        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================

        private void disableControls(Control[] controls)
        {
            foreach (var item in controls)
            {
                item.IsEnabled = false;
            }
            btnSaveEditsOnItem.Opacity = .4;
        }
        private void showProductDetails(string name, string price, string bPrice,ref string pdStock ,string inStock, string pic)
        {
            txtbxItemName.Text = name;
            txtbxItemMinPrice.Text = price;
            txtbxItemBuyinPrice.Text = bPrice;
            txtbxItemQuantity.Text = inStock;
            Uri imageSource = new Uri(pic);
            imgItemProfilePhoto.Source = new BitmapImage(imageSource);

            pdStock = inStock;
        }
        private void clearProductDetails(TextBox[] txtBxArray, Image img)
        {
            foreach (var item in txtBxArray)
            {
                item.Text = string.Empty;
            }
            img.Source = null;
        }
        private void lstbxCategoryItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstbxCategoryItems.SelectedIndex > 0)
            {
                string name = ((ListBoxItem)lstbxCategoryItems.SelectedValue).Content.ToString();
                char[] chars = { '-', '>' };
               string nameToQueryWith = name.Trim(chars);
                database.selectQuery("SELECT Name,BuyingPrice,MinPrice,RemainingStock,PicturePath FROM " + selected + " WHERE Name = '" + nameToQueryWith + "';");


                pdName = database.sqlDataTable.Rows[0]["Name"].ToString();
                pdPrice = database.sqlDataTable.Rows[0]["MinPrice"].ToString();
                pdBuyingPrice = database.sqlDataTable.Rows[0]["BuyingPrice"].ToString();
                string remStock = database.sqlDataTable.Rows[0]["RemainingStock"].ToString();
                pdPic = database.sqlDataTable.Rows[0]["PicturePath"].ToString();

                showProductDetails(pdName, pdPrice, pdBuyingPrice,ref pdInStock,remStock, pdPic);



                Control[] cntrlArray = { btnSaveEditsOnItem, btnUploadItemImage, txtbxItemName, txtbxItemMinPrice, txtbxItemBuyinPrice, txtbxItemQuantity };
                disableControls(cntrlArray);
            }
            else if (lstbxCategoryItems.SelectedIndex == 0)
            {
                TextBox[] txtbxArray = { txtbxItemName, txtbxItemMinPrice, txtbxItemBuyinPrice, txtbxItemQuantity };
                Image img = imgItemProfilePhoto;

                clearProductDetails(txtbxArray, img);
            }



        }


        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================
        private void refreshListBox(string tableName, ref string selctedCategory)
        {
            selctedCategory = tableName;

            lstbxCategoryItems.Items.Clear();
            ListBoxItem notSelected = new ListBoxItem();

            notSelected.Content = "Select An Item".PadLeft(5);
            lstbxCategoryItems.Items.Add(notSelected);
            lstbxCategoryItems.SelectedIndex = 0;
            database.selectQuery("SELECT Name,MinPrice,RemainingStock,PicturePath FROM " + tableName + ";");

            string[] lbContent = new string[database.numRows];

            for (int i = 0; i < database.numRows; i++)
            {
                lbContent[i] ="-->"+ database.sqlDataTable.Rows[i]["Name"].ToString();
            }

            lbFiller.fillListBox(lbContent, lstbxCategoryItems);
        }
        private void btnEditItemSpecs_Click(object sender, RoutedEventArgs e)
        {
            if (userStatus.ToLower() == "localuser")
            {
               MessageBoxResult result = MessageBox.Show("You Are Currently Logged In as Normal User Without Administrative Privilages.  "
                                 + "Please Click OK To Retry Login or Cancel to Quit","Elevation Required!!",MessageBoxButton.OKCancel,
                                 MessageBoxImage.Hand,MessageBoxResult.Cancel);
                if(result == MessageBoxResult.OK)
                {
                    new LoginWindow().ShowDialog();
                }

            }else if(userStatus.ToLower() == "admin")
            {
                btnUploadItemImage.IsEnabled = true;
                btnSaveEditsOnItem.IsEnabled = true;
                txtbxItemName.IsEnabled = true;
                txtbxItemMinPrice.IsEnabled = true;
                txtbxItemBuyinPrice.IsEnabled = true;
                txtbxItemQuantity.IsEnabled = true;

                btnSaveEditsOnItem.Opacity = 1;
            }
        }
        private void btnSaveEditsOnItem_Click(object sender, RoutedEventArgs e)
        {
            //get the current starting stock and picturePath value from db
            string name = ((ListBoxItem)lstbxCategoryItems.SelectedValue).Content.ToString();
            char[] chars = { '-', '>' };
            string nameToQueryWith = name.Trim(chars);
            database.selectQuery("SELECT StartingStock,RemainingStock FROM " + selected + " WHERE Name = '" + nameToQueryWith + "';");
            double currentStartingStock;
            double.TryParse(database.sqlDataTable.Rows[0]["StartingStock"].ToString(), out currentStartingStock);

            //get the added stock from the editor UI 


            double editedQty;
            double currentQTY;

            double.TryParse(txtbxItemQuantity.Text, out editedQty);
            double.TryParse(database.sqlDataTable.Rows[0]["RemainingStock"].ToString(), out currentQTY);
            double surplusStock;
            if (currentQTY < 0)
            {
                surplusStock= editedQty;
            }
            else 
            surplusStock = editedQty - currentQTY;
           
            //calculate for the value you will now use to replace startingstock in database
            double stockToInsert = surplusStock + currentStartingStock;
            //now update the database with the details of the edit
            database.updateQuery("UPDATE " + selected +
                                " SET Name = '" + txtbxItemName.Text +
                                "' ,MinPrice = '" + double.Parse(txtbxItemMinPrice.Text)+
                                "' ,BuyingPrice = '" + double.Parse(txtbxItemBuyinPrice.Text) +
                                "' ,StartingStock = '" + stockToInsert +
                                "' ,PicturePath = '" + pdPic +                              
                                "' WHERE Name = '" + pdName + "';");

            database.updateQuery("UPDATE " + selected + " SET RemainingStock = StartingStock - SoldStock;");
            


            MessageBox.Show(database.successMessage);

            Control[] cntrlArray = { btnSaveEditsOnItem, btnUploadItemImage, txtbxItemName, txtbxItemBuyinPrice, txtbxItemMinPrice, txtbxItemQuantity };
            disableControls(cntrlArray);

            refreshListBox(selected, ref selected);

        }
        private void btnUploadItemImage_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage(selected);

            Uri newSource = new Uri(imageUploader.gsNewimagepath);
            imgItemProfilePhoto.Source = new BitmapImage(newSource);
            pdPic = imageUploader.gsNewimagepath;
        }

        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================

        private void txtbxItemQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            double editedQty;
            double currentQTY;

            double.TryParse(txtbxItemQuantity.Text, out editedQty);
            double.TryParse(pdInStock, out currentQTY);

            if (editedQty < currentQTY)
            {
                txtbxItemQuantity.Foreground = Brushes.Red;
            }
            else txtbxItemQuantity.Foreground = Brushes.Black;

        }
        private void txtbxItemQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            double editedQty;
            double currentQTY;

            double.TryParse(txtbxItemQuantity.Text, out editedQty);
            double.TryParse(pdInStock, out currentQTY);

            if (editedQty < currentQTY)
            {

                MessageBoxResult result = MessageBox.Show("Please Note That The Stock Entered Cannot Be Less Than The Remaining Stock", "Error In Input", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //txtbxItemQuantity.Focus();
                }
            }
        }
        private void txtbxItemMinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double editedMinPrice;
            double currentBuyPrice;

            double.TryParse(txtbxItemMinPrice.Text, out editedMinPrice);
            double.TryParse(txtbxItemBuyinPrice.Text, out currentBuyPrice);

            if (editedMinPrice < currentBuyPrice)
            {
                txtbxItemMinPrice.Foreground = Brushes.Red;
            }
            else txtbxItemMinPrice.Foreground = Brushes.Black;

        }
        private void txtbxItemMinPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            double editedMinPrice;
            double currentBuyPrice;

            double.TryParse(txtbxItemMinPrice.Text, out editedMinPrice);
            double.TryParse(txtbxItemBuyinPrice.Text, out currentBuyPrice);

            if (editedMinPrice < currentBuyPrice)
            {
                MessageBoxResult result = MessageBox.Show("Please Note That The Minimum Price Entered Cannot Be Less Than The Buying Price", "Error In Input", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    //txtbxItemQuantity.Focus();
                }
            }
        }

        private void InventoryEditorUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

       
    }
}
