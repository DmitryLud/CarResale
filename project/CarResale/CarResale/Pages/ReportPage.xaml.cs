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
using CarResale.Windows;

namespace CarResale.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private bool _isReportViewerLoaded;
        private ReportDataSource _reportDataSource1;
        private CarResaleDataSet _carResaleDataSet;
        CarResaleDataSetTableAdapters.SpecificDayReportTableAdapter _specificDayReportTableAdapter;

        public ReportPage()
        {
            InitializeComponent();

            _reportDataSource1 = new ReportDataSource();
            _carResaleDataSet = new CarResaleDataSet();
            _specificDayReportTableAdapter = new CarResaleDataSetTableAdapters.SpecificDayReportTableAdapter();

            ReportView.Load += (s, e) => ReportViewer_Load();
            ReportBtn.Click += (s, e) => {
                try
                {
                    CreateReport(DateTime.Parse(DateTB.Text));
                }
                catch(Exception) 
                {
                    new InfoWindow("Внимание","Введите корректную дату!").ShowDialog();
                }};

        }

        private void CreateReport(DateTime date)
        {
            _specificDayReportTableAdapter.ClearBeforeFill = true;
            _specificDayReportTableAdapter.Fill(_carResaleDataSet.SpecificDayReport, date);

            ReportView.RefreshReport();
        }

        private void ReportViewer_Load()
        {
            if (!_isReportViewerLoaded)
            {
                _carResaleDataSet.BeginInit();

                _reportDataSource1.Name = "DataSet"; 
                _reportDataSource1.Value = _carResaleDataSet.SpecificDayReport;
                ReportView.LocalReport.DataSources.Add(_reportDataSource1);
                ReportView.LocalReport.ReportEmbeddedResource = "CarResale.Reports.DailyReport.rdlc";

                _carResaleDataSet.EndInit();


                _specificDayReportTableAdapter.ClearBeforeFill = true;
                _specificDayReportTableAdapter.Fill(_carResaleDataSet.SpecificDayReport, DateTime.Parse("03.03.2022"));

                ReportView.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
