using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Electricals_PointOfSale.Views;
using System.Data;
using System.Data.SQLite;
using Electricals_PointOfSale.Models;
using System.Windows.Media;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for ItemsAdding.xaml
    /// </summary>
    public partial class ItemsAdding : Window
            {
        UploadImage imageUploader = new UploadImage();
        ControlsCleaner cleanControls = new ControlsCleaner();
        AddedItemsImagesPaths pathKeeper = new AddedItemsImagesPaths();
        DataBaseHandler database = new DataBaseHandler();
              
        public ItemsAdding()
        {
            InitializeComponent();
        }
        private void _callShrinker(Grid grid)
        {
            GridResizer resizer = new GridResizer();
            resizer.shrinkGrids(new Grid[] { grdEmptyDetail,grdNewAdhesiveDetails,grdNewChandelierDetails,
                grdNewExtensionDetails,grdNewFittingDetails,grdNewJackDetails,grdNewLampDetails,
                grdNewLightDetails,grdNewMiscDetails,grdNewPipeDetails,grdNewPowerCordDetails,
                grdNewSconceDetails,grdNewSocketDetails,grdNewSwitchDetails,grdNewUtilityDetails}, grid);
        }
        private void _callButtonsManager(Button btn)
        {
            ButtonsStyle btnStyler = new ButtonsStyle();
           btnStyler.styleAddingButtonsButton(new Button[] {btnCategoriesAdhesives,btnCategoriesChandeliers,btnCategoriesExtensions,btnCategoriesFittings,
          btnCategoriesJacks,btnCategoriesLamps,btnCategoriesLights,btnCategoriesMiscellaneous,btnCategoriesPiping,btnCategoriesPowerCords,
          btnCategoriesSconces,btnCategoriesSockets,btnCategoriesSwitches,btnCategoriesUtilities}, btn);
        }
        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================
        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btnCloseItemsAdder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================
        private void btnCategoriesLights_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewLightDetails);
            _callButtonsManager(btnCategoriesLights);
            
        }      
        private void btnCategoriesSwitches_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewSwitchDetails);
            _callButtonsManager(btnCategoriesSwitches);

        }
        private void btnCategoriesSockets_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewSocketDetails);
            _callButtonsManager(btnCategoriesSockets);
        }
        private void btnCategoriesPowerCords_Click_1(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewPowerCordDetails);
            _callButtonsManager(btnCategoriesPowerCords);

        }
        private void btnCategoriesLamps_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewLampDetails);
            _callButtonsManager(btnCategoriesLamps);

        }
        private void btnCategoriesJacks_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewJackDetails);
            _callButtonsManager(btnCategoriesJacks);

        }
        private void btnCategoriesMiscellaneous_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewMiscDetails);
            _callButtonsManager(btnCategoriesMiscellaneous);

        }
        private void btnCategoriesPiping_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewPipeDetails);
            _callButtonsManager(btnCategoriesPiping);

        }
        private void btnCategoriesUtilities_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewUtilityDetails);
            _callButtonsManager(btnCategoriesUtilities);

        }
        private void btnCategoriesSconces_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewSconceDetails);
            _callButtonsManager(btnCategoriesSconces);
        }
        private void btnCategoriesFittings_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewFittingDetails);
            _callButtonsManager(btnCategoriesFittings);
        }
        private void btnCategoriesAdhesives_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewAdhesiveDetails);
            _callButtonsManager(btnCategoriesAdhesives);
        }
        private void btnCategoriesExtensions_Click(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewExtensionDetails);
            _callButtonsManager(btnCategoriesExtensions);
        }
        private void btnCategoriesChandeliers_Click_1(object sender, RoutedEventArgs e)
        {
            _callShrinker(grdNewChandelierDetails);
            _callButtonsManager(btnCategoriesChandeliers);
        }



        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================


        private void btnUploadLightPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Lights");
            pathKeeper.lightImage = imageUploader.gsNewimagepath;

            if (pathKeeper.lightImage != string.Empty)
            { 
            var myUriSource = new System.Uri(pathKeeper.lightImage);
            imgNewLightProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewLightProfilePic.Source = new BitmapImage(myUriSource);
            }
        }
        private void btnUploadSwitchPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Switches");
            pathKeeper.SwitchImage = imageUploader.gsNewimagepath;
            if (pathKeeper.SwitchImage != string.Empty)
            {
                var myUriSource = new System.Uri(pathKeeper.SwitchImage);
                imgNewLightProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
                imgNewSwitchProfilePic.Source = new BitmapImage(myUriSource);
            }
        }
        private void btnUploadSocketPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Sockets");
            pathKeeper.SocketImage = imageUploader.gsNewimagepath;
            if (pathKeeper.SconceImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.SocketImage);
            imgNewSocketProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewSocketProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadPowerCordPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("PowerCords");
            pathKeeper.PowerCordImage = imageUploader.gsNewimagepath;
            if (pathKeeper.PowerCordImage != string.Empty) { 
            var myUriSource = new System.Uri(pathKeeper.PowerCordImage);
            imgNewPowerCordProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewPowerCordProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadLampPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Lamps");
            pathKeeper.LampImage = imageUploader.gsNewimagepath;
            if (pathKeeper.LampImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.LampImage);
            imgNewLampProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewLampProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadJackPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Jacks");
            pathKeeper.JackImage = imageUploader.gsNewimagepath;
            if (pathKeeper.JackImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.JackImage);
            imgNewJackProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewJackProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadMiscPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Misc");
            pathKeeper.MiscImage = imageUploader.gsNewimagepath;
            if (pathKeeper.MiscImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.MiscImage);
            imgNewMiscProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewMiscProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadPipePhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Pipes");
            pathKeeper.PipeImage = imageUploader.gsNewimagepath;
            if (pathKeeper.PipeImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.PipeImage);
            imgNewPipeProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewPipeProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadUtilityPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Utilities");
            pathKeeper.UtilityImage = imageUploader.gsNewimagepath;
            if (pathKeeper.UtilityImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.UtilityImage);
            imgNewUtilityProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewUtilityProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadSconcePhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Sconces");
            pathKeeper.SconceImage = imageUploader.gsNewimagepath;
            if (pathKeeper.SconceImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.SconceImage);
            imgNewSconceProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewSconceProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadFittingPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Fittings");
            pathKeeper.FittingImage = imageUploader.gsNewimagepath;
            if (pathKeeper.FittingImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.FittingImage);
            imgNewFittingProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewFittingProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadAdhesivePhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Adhesives");
            pathKeeper.AdhesiveImage = imageUploader.gsNewimagepath;
            if (pathKeeper.AdhesiveImage != string.Empty) { 

            var myUriSource = new System.Uri(pathKeeper.AdhesiveImage);
            imgNewAdhesiveProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
            imgNewAdhesiveProfilePic.Source = new BitmapImage(myUriSource);
        }
        }
        private void btnUploadExtensionPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Extensions");
            pathKeeper.ExtensionImage = imageUploader.gsNewimagepath;
            if (pathKeeper.ExtensionImage != string.Empty)
            {
                var myUriSource = new System.Uri(pathKeeper.ExtensionImage);
                imgNewExtensionProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
                imgNewExtensionProfilePic.Source = new BitmapImage(myUriSource);
            }
        }
        private void btnUploadChandelierPhoto_Click(object sender, RoutedEventArgs e)
        {
            imageUploader.uploadImage("Chandeliers");
            pathKeeper.ChandelierImage = imageUploader.gsNewimagepath;
            if (pathKeeper.ChandelierImage != string.Empty)
            {

                var myUriSource = new System.Uri(pathKeeper.ChandelierImage);
                imgNewChandelierProfilePic.Stretch = System.Windows.Media.Stretch.Fill;
                imgNewChandelierProfilePic.Source = new BitmapImage(myUriSource);
            }
        }


        //==========================================================================================================================
        //==========================================================================================================================
        //==========================================================================================================================
            private bool checkValues(TextBox[] textboxes)
        {
            
           for (int i = 0; i < textboxes.Length; i++)
            {
                if (textboxes[i].Text == string.Empty)
                {
                    return  false;
                    
                }
                else return  true;
                
            }
            return false;
        }    


        private void btnSaveNewLight_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] lightTextBoxes = { txtbxNewLightName, txtbxNewLightType, txtbxNewLightWatts, txtbxNewLightCompany,
                                            txtbxNewLightMinPrice,txtbxNewLightBuyingPrice,txtbxNewLightStockQTY};

            if (imgNewLightProfilePic.Source != null && checkValues(lightTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewLightName.Text,
                txtbxNewLightType.Text,
                double.Parse(txtbxNewLightWatts.Text),
                txtbxNewLightCompany.Text,
                double.Parse(txtbxNewLightMinPrice.Text),
                double.Parse(txtbxNewLightBuyingPrice.Text),
                double.Parse(txtbxNewLightStockQTY.Text),
                pathKeeper.lightImage};
                database.insertQuery("INSERT INTO Lights(Name,Type,Watts,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7);", parameterArray);
                database.updateQuery("UPDATE Lights SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewLightName.Text , "Lights"};
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);",pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(lightTextBoxes, Brushes.White);
                    imgNewLightProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
            

        }
        private void btnSaveNewSwitch_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] SwitchTextBoxes = { txtbxNewSwitchName, txtbxNewSwitchType, txtbxNewSwitchCompany,
                                            txtbxNewSwitchMinPrice,txtbxNewSwitchBuyingPrice,txtbxNewSwitchStockQTY};

            if (imgNewSwitchProfilePic.Source != null &&checkValues(SwitchTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewSwitchName.Text,
                txtbxNewSwitchType.Text,
                txtbxNewSwitchCompany.Text,
                double.Parse(txtbxNewSwitchMinPrice.Text),
                double.Parse(txtbxNewSwitchBuyingPrice.Text),
                double.Parse(txtbxNewSwitchStockQTY.Text),
                pathKeeper.SwitchImage};
                database.insertQuery("INSERT INTO Switches(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Switches SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewSwitchName.Text, "Switches" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(SwitchTextBoxes, Brushes.White);
                    imgNewSwitchProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewSocket_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] SocketTextBoxes = { txtbxNewSocketName, txtbxNewSocketType, txtbxNewSocketCompany,
                                            txtbxNewSocketMinPrice,txtbxNewSocketBuyingPrice,txtbxNewSocketStockQTY};

            if (imgNewSocketProfilePic.Source != null && checkValues(SocketTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewSocketName.Text,
                txtbxNewSocketType.Text,
                txtbxNewSocketCompany.Text,
                double.Parse(txtbxNewSocketMinPrice.Text),
                double.Parse(txtbxNewSocketBuyingPrice.Text),
                double.Parse(txtbxNewSocketStockQTY.Text),
                pathKeeper.SocketImage};
                database.insertQuery("INSERT INTO Sockets(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Sockets SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewSocketName.Text, "Sockets" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                 
                   cleanControls.clearTextBoxes(SocketTextBoxes, Brushes.White);
                    imgNewSocketProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewPowerCord_Click(object sender, RoutedEventArgs e)
        {

            TextBox[] PowerCordTextBoxes = { txtbxNewPowerCordName, txtbxNewPowerCordApplication, txtbxNewPowerCordCompany,
                                            txtbxNewPowerCordMinPrice,txtbxNewPowerCordBuyingPrice,txtbxNewPowerCordStockQTY};
            if (imgNewPowerCordProfilePic.Source != null && checkValues(PowerCordTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewPowerCordName.Text,
                txtbxNewPowerCordApplication.Text,
                txtbxNewPowerCordCompany.Text,
                double.Parse(txtbxNewPowerCordMinPrice.Text),
                double.Parse(txtbxNewPowerCordBuyingPrice.Text),
                double.Parse(txtbxNewPowerCordStockQTY.Text),
                pathKeeper.PowerCordImage};
                database.insertQuery("INSERT INTO PowerCords(Name,Application,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE PowerCords SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewPowerCordName.Text, "PowerCords" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                   
                   cleanControls.clearTextBoxes(PowerCordTextBoxes, Brushes.White);
                    imgNewPowerCordProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewLamp_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] LampTextBoxes = { txtbxNewLampName, txtbxNewLampType, txtbxNewLampCompany,
                                            txtbxNewLampMinPrice,txtbxNewLampBuyingPrice,txtbxNewLampStockQTY};

            if (imgNewLampProfilePic.Source != null && checkValues(LampTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewLampName.Text,
                txtbxNewLampType.Text,
                txtbxNewLampCompany.Text,
                double.Parse(txtbxNewLampMinPrice.Text),
                double.Parse(txtbxNewLampBuyingPrice.Text),
                double.Parse(txtbxNewLampStockQTY.Text),
                pathKeeper.LampImage};
                database.insertQuery("INSERT INTO Lamps(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Lamps SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewLampName.Text, "Lamps" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                   
                   cleanControls.clearTextBoxes(LampTextBoxes, Brushes.White);
                    imgNewLampProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewJack_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] JackTextBoxes = { txtbxNewJackName, txtbxNewJackType, txtbxNewJackCompany,
                                            txtbxNewJackMinPrice,txtbxNewJackBuyingPrice,txtbxNewJackStockQTY};

            if (imgNewJackProfilePic.Source != null && checkValues(JackTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewJackName.Text,
                txtbxNewJackType.Text,
                txtbxNewJackCompany.Text,
                double.Parse(txtbxNewJackMinPrice.Text),
                double.Parse(txtbxNewJackBuyingPrice.Text),
                double.Parse(txtbxNewJackStockQTY.Text),
                pathKeeper.JackImage};
                database.insertQuery("INSERT INTO Jacks(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Jacks SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewJackName.Text, "Jacks" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(JackTextBoxes, Brushes.White);
                    imgNewJackProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewMisc_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] MiscTextBoxes = { txtbxNewMiscName,txtbxNewMiscCompany,
                                            txtbxNewMiscMinPrice,txtbxNewMiscBuyingPrice,txtbxNewMiscStockQTY};

            if (imgNewMiscProfilePic.Source != null && checkValues(MiscTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewMiscName.Text,
                txtbxNewMiscCompany.Text,
                double.Parse(txtbxNewMiscMinPrice.Text),
                double.Parse(txtbxNewMiscBuyingPrice.Text),
                double.Parse(txtbxNewMiscStockQTY.Text),
                pathKeeper.MiscImage};
                database.insertQuery("INSERT INTO Miscellaneous(Name,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5)", parameterArray);
                database.updateQuery("UPDATE Miscellaneous SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewMiscName.Text, "Miscellaneous" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(MiscTextBoxes, Brushes.White);
                    imgNewMiscProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewPipe_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] PipeTextBoxes = { txtbxNewPipeName, txtbxNewPipeColor,txtbxNewPipeDiameter, txtbxNewPipeCompany,
                                            txtbxNewPipeMinPrice,txtbxNewPipeBuyingPrice,txtbxNewPipeStockQTY};

            if (imgNewPipeProfilePic.Source != null && checkValues(PipeTextBoxes) != false )
            {
                object[] parameterArray = {
                txtbxNewPipeName.Text,
                txtbxNewPipeColor.Text,
                double.Parse(txtbxNewPipeDiameter.Text),
                txtbxNewPipeCompany.Text,
                double.Parse(txtbxNewPipeMinPrice.Text),
                double.Parse(txtbxNewPipeBuyingPrice.Text),
                double.Parse(txtbxNewPipeStockQTY.Text),
                pathKeeper.PipeImage};
                database.insertQuery("INSERT INTO Pipes(Name,Color,Diameter,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", parameterArray);
                database.updateQuery("UPDATE Pipes SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewPipeName.Text, "Pipes" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(PipeTextBoxes, Brushes.White);
                    imgNewPipeProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewUtility_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] UtilityTextBoxes = { txtbxNewUtilityName, txtbxNewUtilityApplication, txtbxNewUtilityCompany,
                                            txtbxNewUtilityMinPrice,txtbxNewUtilityBuyingPrice,txtbxNewUtilityStockQTY};
            if (imgNewUtilityProfilePic.Source != null && checkValues(UtilityTextBoxes) != false ) 
            {
                object[] parameterArray = {
                txtbxNewUtilityName.Text,
                txtbxNewUtilityCompany.Text,
                txtbxNewUtilityApplication.Text,
                double.Parse(txtbxNewUtilityMinPrice.Text),
                double.Parse(txtbxNewUtilityBuyingPrice.Text),
                double.Parse(txtbxNewUtilityStockQTY.Text),
                pathKeeper.UtilityImage};
                database.insertQuery("INSERT INTO Utilities(Name,Company,Application,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Utilities SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewUtilityName.Text, "Utilities" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(UtilityTextBoxes, Brushes.White);
                    imgNewUtilityProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Fill all Fields for this Product First Including an Image");
        }
        private void btnSaveNewSconce_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] SconceTextBoxes = { txtbxNewSconceName, txtbxNewSconceType, txtbxNewSconceCompany,
                                            txtbxNewSconceMinPrice,txtbxNewSconceBuyingPrice,txtbxNewSconceStockQTY};

            if (imgNewSconceProfilePic.Source != null &&checkValues(SconceTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewSconceName.Text,
                txtbxNewSconceType.Text,
                txtbxNewSconceCompany.Text,
                double.Parse(txtbxNewSconceMinPrice.Text),
                double.Parse(txtbxNewSconceBuyingPrice.Text),
                double.Parse(txtbxNewSconceStockQTY.Text),
                pathKeeper.SconceImage};
                database.insertQuery("INSERT INTO Sconces(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Sconces SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewSconceName.Text, "Sconces" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                 
                   cleanControls.clearTextBoxes(SconceTextBoxes, Brushes.White);
                    imgNewSconceProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewFitting_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] FittingTextBoxes = { txtbxNewFittingName, txtbxNewFittingApplication, txtbxNewFittingCompany,
                                            txtbxNewFittingMinPrice,txtbxNewFittingBuyingPrice,txtbxNewFittingStockQTY};

            if (imgNewFittingProfilePic.Source != null && checkValues(FittingTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewFittingName.Text,
                txtbxNewFittingCompany.Text,
                txtbxNewFittingApplication.Text,
                double.Parse(txtbxNewFittingMinPrice.Text),
                double.Parse(txtbxNewFittingBuyingPrice.Text),
                double.Parse(txtbxNewFittingStockQTY.Text),
                pathKeeper.FittingImage};
                database.insertQuery("INSERT INTO Fittings(Name,Company,Application,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Fittings SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewFittingName.Text, "Fittings" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(FittingTextBoxes, Brushes.White);
                    imgNewFittingProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewAdhesive_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] AdhesiveTextBoxes = { txtbxNewAdhesiveName, txtbxNewAdhesiveType, txtbxNewAdhesiveCompany,
                                            txtbxNewAdhesiveMinPrice,txtbxNewAdhesiveBuyingPrice,txtbxNewAdhesiveStockQTY};

            if (imgNewAdhesiveProfilePic.Source != null  && checkValues(AdhesiveTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewAdhesiveName.Text,
                txtbxNewAdhesiveType.Text,
                txtbxNewAdhesiveCompany.Text,
                double.Parse(txtbxNewAdhesiveMinPrice.Text),
                double.Parse(txtbxNewAdhesiveBuyingPrice.Text),
                double.Parse(txtbxNewAdhesiveStockQTY.Text),
                pathKeeper.AdhesiveImage};
                database.insertQuery("INSERT INTO Adhesives(Name,Type,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Adhesives SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewAdhesiveName.Text, "Adhesives" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(AdhesiveTextBoxes, Brushes.White);
                    imgNewAdhesiveProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewExtension_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] ExtensionTextBoxes = { txtbxNewExtensionName, txtbxNewExtensionSockets, txtbxNewExtensionCompany,
                                            txtbxNewExtensionMinPrice,txtbxNewExtensionBuyingPrice,txtbxNewExtensionStockQTY};

            if (imgNewExtensionProfilePic.Source != null && checkValues(ExtensionTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewExtensionName.Text,
                double.Parse(txtbxNewExtensionSockets.Text),
                txtbxNewExtensionCompany.Text,
                double.Parse(txtbxNewExtensionMinPrice.Text),
                double.Parse(txtbxNewExtensionBuyingPrice.Text),
                double.Parse(txtbxNewExtensionStockQTY.Text),
                pathKeeper.ExtensionImage};
                database.insertQuery("INSERT INTO Extensions(Name,Sockets,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", parameterArray);
                database.updateQuery("UPDATE Extensions SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewExtensionName.Text, "Extensions" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                    
                   cleanControls.clearTextBoxes(ExtensionTextBoxes, Brushes.White);
                    imgNewExtensionProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }
        private void btnSaveNewChandelier_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] ChandelierTextBoxes = { txtbxNewChandelierName,txtbxNewChandelierType,txtbxNewChandelierUniqueID,
                                                       txtbxNewChandelierArms, txtbxNewChandelierCompany,txtbxNewChandelierMinPrice,
                                                      txtbxNewChandelierBuyingPrice,txtbxNewChandelierStockQTY};

            if (imgNewChandelierProfilePic.Source != null && checkValues(ChandelierTextBoxes) != false)
            {
                object[] parameterArray = {
                txtbxNewChandelierName.Text,
                txtbxNewChandelierType.Text,
                txtbxNewChandelierUniqueID.Text,
                double.Parse(txtbxNewChandelierArms.Text),
                txtbxNewChandelierCompany.Text,
                double.Parse(txtbxNewChandelierMinPrice.Text),
                double.Parse(txtbxNewChandelierBuyingPrice.Text),
                double.Parse(txtbxNewChandelierStockQTY.Text),
                pathKeeper.ChandelierImage};
                database.insertQuery("INSERT INTO Chandeliers(Name,Type,UniqueID,Arms,Company,MinPrice,BuyingPrice,StartingStock,PicturePath)" +
                                    " VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", parameterArray);
                database.updateQuery("UPDATE Chandeliers SET RemainingStock = StartingStock - SoldStock;");
                object[] pArray = { txtbxNewChandelierName.Text, "Chandeliers" };
                database.insertQuery("INSERT INTO ItemsTableNames(ProductName, ProductTable) VALUES(@p0,@p1);", pArray);
                MessageBox.Show(database.successMessage);

                if (database.successMessage == "Success")
                {
                   
                   cleanControls.clearTextBoxes(ChandelierTextBoxes, Brushes.White);
                    imgNewChandelierProfilePic.Source = null;

                }
            }
            else MessageBox.Show("Please Add an Image to this Product First");
        }

        private void ItemsAddingUI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

        }
    }
}
