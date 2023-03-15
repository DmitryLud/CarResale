using CarResale.DBModel;
using CarResale.Windows;
using CarResale.WordHelper;
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

namespace CarResale.Pages
{
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();

            ModelCB.IsEnabled = false;

            DG.ItemsSource = CarResaleEntities.GetContext().Orders.ToList();
            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new OrderAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new OrderAddPage(DG.SelectedItem as Order)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaulFilter(); };
            SearchBtn.Click += (s, e) => { Search(); };
            ContractBtn.Click += (s, e) => { Contract.ReplaceText(DG.SelectedItem as Order); };

            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();

            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };
            ModelCB.SelectionChanged += (s, e) => { SelectedModel(); };
            PriceFromTB.LostFocus += (s, e) => { SelectedPrice(); };
            PriceToTB.LostFocus += (s, e) => { SelectedPrice(); };
            SaleDateTB.LostFocus += (s, e) => { SelectedDate(); };

        }
        private void SetDefaulFilter()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Orders.ToList();

            SearchTB.Text = null;
            MarkCB.Text = null;
            ModelCB.Text = null;
            SaleDateTB.Text = null;
            PriceFromTB.Text = null;
            PriceToTB.Text = null;

        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null)
            {
                ModelCB.IsEnabled = false;
                return;
            }

            int selectedItem = (MarkCB.SelectedValue as Mark).ID;
            DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(x => x.Car.Model.MarkID == selectedItem).ToList();

            ModelCB.IsEnabled = true;
            ModelCB.ItemsSource = CarResaleEntities.GetContext().Models.Where(x => x.MarkID == selectedItem).ToList();
        }

        private void SelectedModel()
        {
            if (ModelCB.SelectedValue == null) return;
            string selectedItem = (ModelCB.SelectedValue as Model).Name;
            DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(x => x.Car.Model.Name == selectedItem).ToList();
        }

        private void SelectedPrice()
        {
            string selectedItemFrom = PriceFromTB.Text;
            string selectedItemTo = PriceToTB.Text;
            int priceFrom = 0;
            int priceTo = 0;
            if (int.TryParse(selectedItemFrom, out priceFrom))
                DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(x => x.Sale_price >= priceFrom).ToList();
            if (int.TryParse(selectedItemTo, out priceTo) && priceTo > priceFrom)
                DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(x => x.Sale_price <= priceTo).ToList();
        }

        private void SelectedDate()
        {
            DateTime selected;
            if (!DateTime.TryParse(SaleDateTB.Text, out selected)) return;
            DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(x => x.Sale_date == selected).ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Order>).Where(
                x => x.Car.VIN.Contains(SearchTB.Text) ||
                x.Car.Model.Name.Contains(SearchTB.Text) ||
                x.Car.Model.Mark.Name.Contains(SearchTB.Text) ||
                x.Customer.Surname.Contains(SearchTB.Text) ||
                x.Customer.Name.Contains(SearchTB.Text) ||
                x.Customer.Phone.Contains(SearchTB.Text) ||
                x.Customer.Email.Contains(SearchTB.Text) ||
                x.Sale_date.ToString().Contains(SearchTB.Text) ||
                x.Sale_price.ToString().Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (!(bool)new QuestionWindow("Внимание!", "Вы точно хотите удалить выбранные элементы?").ShowDialog()) return;

            var selectedItems = DG.SelectedItems.Cast<Order>().ToList();

            try
            {
                CarResaleEntities.GetContext().Orders.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().SaveChanges();
                DG.ItemsSource = CarResaleEntities.GetContext().Orders.ToList();
                new InfoWindow("Уведомление", "Данные успешно удалены!").ShowDialog();
            }
            catch (Exception ex)
            {
                new InfoWindow("Ошибка", ex.Message).ShowDialog();
            }

        }
    }
}
