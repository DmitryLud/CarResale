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
    public partial class OrderAddPage : Page
    {
        private Order _current = new Order();
        public OrderAddPage(Order selected = null)
        {
            InitializeComponent();

            _current.Sale_date = DateTime.Now;

            if (selected != null)
            {
                _current = selected;
            }

            DataContext = _current;

            SaveBtn.Click += (s, e) => { SaveData(); };
            CancelBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new OrderPage()); };

        }

        private void SaveData()
        {
            try
            {
                _current.Car = CarResaleEntities.GetContext().Cars.First(x => x.VIN == VINTB.Text);
                _current.Customer = CarResaleEntities.GetContext().Customers.First(x => x.Phone == PhoneTB.Text);
            }
            catch (Exception) { }

            StringBuilder errors = new StringBuilder();
            if (_current.Car == null)
                errors.AppendLine("Выберите автомобиль");
            if (_current.Customer == null)
                errors.AppendLine("Выберите клиента");
            if (_current.Sale_date > DateTime.Now)
                errors.AppendLine("Введите дату продажи");
            if (_current.Sale_price < 0)
                errors.AppendLine("Введите корректную цену");

            if (errors.Length > 0)
            {
                new InfoWindow("Ошибка", errors.ToString()).ShowDialog();
                return;
            }

            if (_current.ID == 0)
                CarResaleEntities.GetContext().Orders.Add(_current);

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
