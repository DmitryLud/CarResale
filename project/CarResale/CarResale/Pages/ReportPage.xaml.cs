using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;

namespace CarResale.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private bool _isReportViewerLoaded;

        public ReportPage()
        {
            InitializeComponent();

            ReportView.Load += (s, e) => ReportViewer_Load();

        }

        private void ReportViewer_Load()
        {
            if (!_isReportViewerLoaded)
            {
                ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                CarResaleDataSet carResaleDataSet = new CarResaleDataSet();

                carResaleDataSet.BeginInit();

                reportDataSource1.Name = "DataSet"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = carResaleDataSet.SpecificDayReport;
                ReportView.LocalReport.DataSources.Add(reportDataSource1);
                ReportView.LocalReport.ReportEmbeddedResource = "CarResale.Reports.DailyReport.rdlc";

                carResaleDataSet.EndInit();

                //fill data into adventureWorksDataSet
                CarResaleDataSetTableAdapters.SpecificDayReportTableAdapter specificDayReportTableAdapter = new CarResaleDataSetTableAdapters.SpecificDayReportTableAdapter();
                specificDayReportTableAdapter.ClearBeforeFill = true;
                specificDayReportTableAdapter.Fill(carResaleDataSet.SpecificDayReport, DateTime.Now);

                ReportView.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
