using CarResale.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarResale.Windows;


namespace CarResale.Pages
{
    public partial class CarPage : Page
    {
        public CarPage()
        {
            InitializeComponent();

            ModelCB.IsEnabled = false;

            DG.ItemsSource = CarResaleEntities.GetContext().Cars.ToList();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new CarAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new CarAddPage(DG.SelectedItem as Car)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaulFilter(); };
            SearchBtn.Click += (s, e) => { Search(); };
            ReceiptInvoiceBtn.Click += (s, e) => { new InfoWindow("Информация о приходной накладной", GetReceiptInvoice((DG.SelectedItem as Car).ReceiptInvoiceID)).ShowDialog(); };
            
            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
            FuelTypeCB.ItemsSource = CarResaleEntities.GetContext().Cars.Select(x=>x.FuelType).Distinct().ToList();
            TrimCB.ItemsSource = CarResaleEntities.GetContext().Cars.Select(x=>x.TRIM).Distinct().ToList();
            ColorCB.ItemsSource = CarResaleEntities.GetContext().Cars.Select(x=>x.Color).Distinct().ToList();
            TransmissionCB.ItemsSource = CarResaleEntities.GetContext().Cars.Select(x=>x.Transmission).Distinct().ToList();            

            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };
            ModelCB.SelectionChanged += (s, e) => { SelectedModel(); };
            FuelTypeCB.SelectionChanged += (s, e) => { SelectedFuelType(); };
            TrimCB.SelectionChanged += (s, e) => { SelectedTrim(); };
            ColorCB.SelectionChanged += (s, e) => { SelectedColor(); };
            MileageFromTB.LostFocus += (s, e) => { SelectedMileage(); };
            MileageToTB.LostFocus += (s, e) => { SelectedMileage(); };
            YearTB.TextChanged += (s, e) => { SelectedYear(); };
            TransmissionCB.SelectionChanged += (s, e) => { SelectedTransmission(); };


        }
        private void SetDefaulFilter()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Cars.ToList();

            SearchTB.Text = null;
            FuelTypeCB.Text = null;
            MarkCB.Text = null;
            ModelCB.Text = null;
            TransmissionCB.Text = null;
            ColorCB.Text = null;
            MileageFromTB.Text = null;
            MileageToTB.Text = null;
            TrimCB.Text = null;
            YearTB.Text = null;

        }

        private string GetReceiptInvoice(int id)
        {
            var item = CarResaleEntities.GetContext().ReceiptInvoices.Where(x=>x.ID == id).ToList()[0];
            return String.Format("Дата поступления: {0:MM/dd/yyyy}\nЦена за автомобиль: {1}\nПрочие расходы: {2}\nИтог: {3}",
                item.Date_of_acquisition.Date, item.Acquisistion_price, item.Other_costs, item.Total_acquisistion_price);
        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null)
            {
                ModelCB.IsEnabled = false;
                return;
            }

            int selectedItem = (MarkCB.SelectedValue as Mark).ID;
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Model.MarkID == selectedItem).ToList();

            ModelCB.IsEnabled = true;
            ModelCB.ItemsSource = CarResaleEntities.GetContext().Models.Where(x=>x.MarkID == selectedItem).ToList();
        }

        private void SelectedModel()
        {
            if (ModelCB.SelectedValue == null) return;
            string selectedItem = ModelCB.SelectedValue.ToString();
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Model.Name == selectedItem).ToList();
        }

        private void SelectedFuelType()
        {
            if (FuelTypeCB.SelectedValue == null) return;
            string selectedItem = FuelTypeCB.SelectedValue.ToString();
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.FuelType == selectedItem).ToList();
        }

        private void SelectedTrim()
        {
            if (TrimCB.SelectedValue == null) return;
            string selectedItem = TrimCB.SelectedValue.ToString();
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.TRIM == selectedItem).ToList();
        }

        private void SelectedColor()
        {
            if (ColorCB.SelectedValue == null) return;
            string selectedItem = ColorCB.SelectedValue.ToString();
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Color == selectedItem).ToList();
        }

        private void SelectedMileage()
        {
            string selectedItemFrom = MileageFromTB.Text;
            string selectedItemTo = MileageToTB.Text;
            int mileageFrom = 0;
            int mileageTo = 0;
            if (int.TryParse(selectedItemFrom, out mileageFrom)) 
                DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Mileage >= mileageFrom).ToList();
            if (int.TryParse(selectedItemTo, out mileageTo) && mileageTo > mileageFrom)
                DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Mileage <= mileageTo).ToList();
        }

        private void SelectedTransmission()
        {
            if (TransmissionCB.SelectedValue == null) return;
            string selectedItem = TransmissionCB.SelectedValue.ToString();
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Transmission == selectedItem).ToList();
        }

        private void SelectedYear()
        {
            string selectedItem = YearTB.Text;
            int year = 0;
            if (selectedItem.Length != 4 || !int.TryParse(selectedItem, out year)) return;
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(x => x.Year == year).ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Car>).Where(
                x => x.VIN.Contains(SearchTB.Text) ||
                x.Model.Name.Contains(SearchTB.Text) ||
                x.Model.Mark.Name.Contains(SearchTB.Text) ||
                x.Mileage.ToString().Contains(SearchTB.Text) ||
                x.FuelType.Contains(SearchTB.Text) ||
                x.TRIM.Contains(SearchTB.Text) ||
                x.Transmission.Contains(SearchTB.Text) ||
                x.Year.ToString().Contains(SearchTB.Text) ||
                x.Color.Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (!(bool)new QuestionWindow("Внимание!", "Вы точно хотите удалить выбранные элементы?").ShowDialog()) return;

            var selectedItems = DG.SelectedItems.Cast<Car>().ToList();
            var selectedReceiptInvoiceIDs = selectedItems.Select(y => y.ReceiptInvoiceID).ToList();

            try
            {
                CarResaleEntities.GetContext().Cars.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().ReceiptInvoices.RemoveRange(CarResaleEntities.GetContext().ReceiptInvoices.Where(x=>selectedReceiptInvoiceIDs.Contains(x.ID)));
                CarResaleEntities.GetContext().SaveChanges();
                DG.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
                new InfoWindow("Уведомление", "Данные успешно удалены!").ShowDialog();
            }
            catch (Exception ex)
            {
                new InfoWindow("Ошибка", ex.Message).ShowDialog();
            }

        }
    }
}
