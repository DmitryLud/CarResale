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
using System.Text.RegularExpressions;
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
        CarResaleDataSetTableAdapters.SpecificPeriodReportTableAdapter _specificPeriodReportTableAdapter;

        public ReportPage()
        {
            InitializeComponent();

            _reportDataSource1 = new ReportDataSource();
            _carResaleDataSet = new CarResaleDataSet();
            _specificDayReportTableAdapter = new CarResaleDataSetTableAdapters.SpecificDayReportTableAdapter();
            _specificPeriodReportTableAdapter = new CarResaleDataSetTableAdapters.SpecificPeriodReportTableAdapter();


            ReportView.Load += (s, e) => ReportViewer_Load();
            ReportBtn.Click += (s, e) => {
                try
                {
                    if (!IsValidDate(DateTB.Text)) throw new Exception();
                    CreateReport(DateTB.Text);
                }
                catch(Exception) 
                {
                    new InfoWindow("Внимание","Введите корректную дату!").ShowDialog();
                }};

        }

        private bool IsValidDate(string date)
        {
            return Regex.IsMatch(date, @"[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}|[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}-[0-9]{1,2}\.[0-9]{1,2}\.[0-9]{4}");
        }

        private void CreateReport(string date)
        {
            string[] dates = date.Split('-');

            if (dates.Count() == 2)
            {
                _carResaleDataSet.BeginInit();

                _reportDataSource1.Value = _carResaleDataSet.SpecificPeriodReport;
                ReportView.LocalReport.DataSources.Add(_reportDataSource1);
                ReportView.LocalReport.ReportEmbeddedResource = "CarResale.Reports.PeriodReport.rdlc";

                _carResaleDataSet.EndInit();

                _specificPeriodReportTableAdapter.ClearBeforeFill = true;
                _specificPeriodReportTableAdapter.Fill(_carResaleDataSet.SpecificPeriodReport, DateTime.Parse(dates[0]), DateTime.Parse(dates[1]));
            }
            else
            {
                _carResaleDataSet.BeginInit();

                _reportDataSource1.Value = _carResaleDataSet.SpecificDayReport;
                ReportView.LocalReport.DataSources.Add(_reportDataSource1);
                ReportView.LocalReport.ReportEmbeddedResource = "CarResale.Reports.DailyReport.rdlc";

                _carResaleDataSet.EndInit();


                _specificDayReportTableAdapter.ClearBeforeFill = true;
                _specificDayReportTableAdapter.Fill(_carResaleDataSet.SpecificDayReport, DateTime.Parse(date));
            }

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
                _specificDayReportTableAdapter.Fill(_carResaleDataSet.SpecificDayReport, DateTime.Now);

                ReportView.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
