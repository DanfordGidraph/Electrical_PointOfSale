using System.Windows;
using Microsoft.Reporting.WinForms;
using Electricals_PointOfSale.Models;
using System;
using Electricals_PointOfSale.Views;

namespace Electricals_PointOfSale
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        DataBaseHandler database = new DataBaseHandler();
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void dtepckReportDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime datePicked = DateTime.Parse(dtepckReportDate.ToString());
            //MessageBox.Show(datePicked.ToString());
                       
        }
        
        
    }
}
