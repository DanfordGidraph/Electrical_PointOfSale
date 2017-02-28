using Microsoft.Reporting.WinForms;
using System.Data;

namespace Electricals_PointOfSale.Views
{
    class ReportGenerator
    {
        private static ReportGenerator Instance;
        public static ReportGenerator getInstance()
        {
            if (Instance == null)
            {
                return Instance = new ReportGenerator();
            }
            else return Instance;
        }
        private ReportGenerator() { }

        public void generateReport(ReportViewer rViewer, DataTable dTable, DataSet dSet)
        {
            ReportDataSource rds = new ReportDataSource(dSet.ToString(), dTable);
            rViewer.LocalReport.DataSources.Add(rds);
            
            rViewer.RefreshReport();

        }
    }
}
