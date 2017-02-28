using Electricals_PointOfSale.Models;
using System;
using System.Windows;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DataBaseHandler database = new DataBaseHandler();


        //private object accessLevelInstance;
        private string accessLevel;
        private string currentUser;
 
     public LoginWindow()
        {
            InitializeComponent();
        }

        private bool checkCredentials(string username, string password, ref string access)
        {
            database.selectQuery("SELECT Permission,Username,Password FROM Users WHERE Username = '" + username +"';");

            if (database.numRows > 0)
            {
                if (password == string.Empty)
                {
                    access = "LocalUser";
                    currentUser = username;
                    return true;
                }
                else {
                    string permission = database.sqlDataTable.Rows[0]["Permission"].ToString();
                    string inDbPassword = database.sqlDataTable.Rows[0]["Password"].ToString();
                    string inDbUserName = database.sqlDataTable.Rows[0]["Username"].ToString();
                    if (permission == "Admin" && inDbPassword == password && inDbUserName.ToLower() == username.ToLower())
                    {
                        access = "Admin";
                        currentUser = username;
                        return true;
                    }else if(permission == "LocalUser" && inDbPassword == password && inDbUserName.ToLower() == username.ToLower())
                    {
                        access = "LocalUser";
                        currentUser = username;
                        return true;
                    }
                    return false;
                    }
           }else
                return false;
        }
        private void btnCloseLoginWindow_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (checkCredentials(txtbxUsername.Text, txtbxPassword.Password, ref accessLevel) == true){
                UserAccessLevel.getInstance().gsCurrentUserAccessLevel = accessLevel;
                UserAccessLevel.getInstance().gsCurrentUserName = currentUser;
               
                this.Close();
            }
            else
            {
                MessageBox.Show("The Credentials Entered Did NOT Match, Please Revise and Try Again", "Login Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

   
    }
}
