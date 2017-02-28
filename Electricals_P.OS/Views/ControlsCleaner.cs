using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Electricals_PointOfSale.Views
{
    class ControlsCleaner: Window 
    {
        public void clearListBox(ListBox lstBx)
        {
            lstBx.Items.Clear();
        }
        public void clearTextBoxes(TextBox[] txtBoxes, Brush bckGround)
        {
            foreach (var item in txtBoxes)
            {
                item.Text = string.Empty;
                item.Background = bckGround;
            }
        }
        public void clearTextBlocks(TextBlock[] txtBlocks, Brush bckGround)
        {
            foreach (var item in txtBlocks)
            {
                item.Text = string.Empty;
                item.Background = bckGround;
            }
        }
        public void clearImages(Image[] images)
        {
            foreach (var item in images)
            {

                item.Source = null;
            }
        }
    }
}
