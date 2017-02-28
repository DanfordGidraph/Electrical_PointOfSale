using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Electricals_PointOfSale.Models
{
    class UploadImage : Window
    {
        private string newImagePath;
        public string gsNewimagepath {
            get
            {
                return newImagePath;
            }
            set
            {
                newImagePath = value;
            }
         }

        public void uploadImage(string Folder)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Image (*.jpg)|*.jpg|JPEG Image(*.jpeg)|*.jpeg|GIF Image (*.gif)|*.gif|PNG Image (*.png)|*.png";
            dlg.Multiselect = false;
            bool result = false;
            result = (bool)dlg.ShowDialog();
            string oldpath = dlg.FileName;
            string actualName = dlg.SafeFileName;
            string AppData = System.Windows.Forms.Application.UserAppDataPath;
            DirectoryInfo Images = new DirectoryInfo(AppData + "\\Images\\");
            if (!Images.Exists)
            {
                Images.Create();
                MessageBox.Show("Directory Created ");
            }
           string nfpath =  copyToNewLocation(Images, oldpath, actualName, result,Folder);

            gsNewimagepath = nfpath;

        }

        private string copyToNewLocation(DirectoryInfo dir, string oldPath, string actualName, bool result, string folder)
        {
            string newFilePath = string.Empty;
            try
            {
                if (result == true)
                {
                    DirectoryInfo specificPath = new DirectoryInfo(dir + folder + "\\");
                    if (!specificPath.Exists)
                    {
                        specificPath.Create();
                    }
                    string newPath = specificPath.ToString();

                    File.Copy(oldPath, specificPath + actualName, true);
                    newFilePath = newPath + actualName;
                    return newFilePath;
                } else

                return string.Empty;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        private void setImagePath(ref string ImagePath , string path)
        {
            ImagePath = path;
        }
    }
}
