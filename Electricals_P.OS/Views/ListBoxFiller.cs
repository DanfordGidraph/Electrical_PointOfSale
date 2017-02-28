using System.Windows.Controls;
using System;
using Microsoft.CSharp;
using System.Windows;

namespace Electricals_PointOfSale
{
    class ListBoxFiller: Window
    {

        public void fillListBox(string[] content, ListBox lBox)
        {
            for(int x = 0; x < content.Length; x++)
            {
                ListBoxItem lbItem = new ListBoxItem();
                lbItem.Content = content[x];
                lBox.Items.Add(lbItem);

            }

        }

    }
}
