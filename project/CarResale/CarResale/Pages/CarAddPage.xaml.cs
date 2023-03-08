using CarResale.DBModel;
using CarResale.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarResale.Pages
{
    public partial class CarAddPage : Page
    {
        private Car _current = new Car();
        private ReceiptInvoice _receiptInvoice = new ReceiptInvoice();
        public CarAddPage(Car selected = null)
        {

            InitializeComponent();

            ModelCB.IsEnabled = false;

            if (selected != null)
            {
                _current = selected;
                _receiptInvoice = selected.ReceiptInvoice;
                SelectedMark();
            }
            DataContext = _current;
            AcquisistionPriceTB.DataContext = _receiptInvoice;
            DateOfAcquisitionTB.DataContext = _receiptInvoice;
            OtherCostsTB.DataContext = _receiptInvoice;
            TotalAcquisistionPriceTB.DataContext = _receiptInvoice;

            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };

            SaveBtn.Click += (s, e) => { SaveData(); };
            CancelBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new CarPage()); };

            AcquisistionPriceTB.TextChanged += (s, e) => { SetTotalAcquisistionPrice(); };
            OtherCostsTB.TextChanged += (s, e) => { SetTotalAcquisistionPrice(); };

        }

        private void SetTotalAcquisistionPrice()
        {
            if (string.IsNullOrWhiteSpace(AcquisistionPriceTB.Text) ||
                string.IsNullOrWhiteSpace(OtherCostsTB.Text)) return;
            decimal total = decimal.Parse(AcquisistionPriceTB.Text) + decimal.Parse(OtherCostsTB.Text);
            TotalAcquisistionPriceTB.Text = total.ToString();
            _receiptInvoice.Total_acquisistion_price = total;
        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null)
            {
                ModelCB.IsEnabled = false;
                return;
            }

            int selectedItem = (MarkCB.SelectedItem as Mark).ID;
            ModelCB.IsEnabled = true;
            ModelCB.ItemsSource = CarResaleEntities.GetContext().Models.Where(x=>x.MarkID == selectedItem).ToList();
        }

        private void SaveData()
        {
            StringBuilder errors = new StringBuilder();
            if (_current.Model == null)
                errors.AppendLine("Выберите модель");
            if (_current.VIN.Length != 17)
                errors.AppendLine("Введите VIN");
            if (CarResaleEntities.GetContext().Cars.Where(x=>x.VIN == _current.VIN).Count() != 0)
                errors.AppendLine("Данный VIN уже зарегистрирован!");
            if (string.IsNullOrWhiteSpace(_current.TRIM))
                errors.Append("Введите комплектацию");
            if (string.IsNullOrWhiteSpace(_current.Transmission))
                errors.Append("Введите тип коробки передач");
            if (string.IsNullOrWhiteSpace(_current.Color))
                errors.Append("Введите цвет");
            if (_current.Year == 0)
                errors.AppendLine("Введите год");
            if (string.IsNullOrWhiteSpace(_current.FuelType))
                errors.Append("Введите тип двигателя");
            if (_receiptInvoice.Date_of_acquisition.Date > DateTime.Now)
                errors.Append("Введите корректную дату");

            if (errors.Length > 0)
            {
                new InfoWindow("Ошибка", errors.ToString()).ShowDialog();
                return;
            }

            if (_current.ID == 0)
            {
                CarResaleEntities.GetContext().ReceiptInvoices.Add(_receiptInvoice);
                _current.ReceiptInvoice = _receiptInvoice;
                CarResaleEntities.GetContext().Cars.Add(_current);
            }
                
            try
            {
                CarResaleEntities.GetContext().SaveChanges();
                new InfoWindow("Уведомление", "Данные успешно сохранены!").ShowDialog();
                CancelBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            catch (Exception ex)
            {
                new InfoWindow("Ошибка", ex.Message).ShowDialog();
            }
        }

    }
}
