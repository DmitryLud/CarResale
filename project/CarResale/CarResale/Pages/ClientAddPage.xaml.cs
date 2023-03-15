using CarResale.DBModel;
using CarResale.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace CarResale.Pages
{
    public partial class ClientAddPage : Page
    {
        private Customer _current = new Customer();
        public ClientAddPage(Customer selected = null)
        {
            InitializeComponent();
            if (selected != null)
            {
                _current = selected;
            }

            DataContext = _current;

            SaveBtn.Click += (s, e) => { SaveData(); };
            CancelBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ClientPage()); };

        }

        private void SaveData()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_current.Surname))
                errors.AppendLine("Введите фамилию");
            if (string.IsNullOrWhiteSpace(_current.Name))
                errors.AppendLine("Введите имя");
            if (string.IsNullOrWhiteSpace(_current.Phone) || !Regex.IsMatch(_current.Phone, @"375[0-9]{9}"))
                errors.AppendLine("Введите номер телефона");
            if (string.IsNullOrWhiteSpace(_current.Email) || !Regex.IsMatch(_current.Email, @"^[a-z0-9](\.?[a-z0-9]){5,}(@gmail\.com)|(@mail\.ru)$"))
                errors.AppendLine("Введите почту");

            if (errors.Length > 0)
            {
                new InfoWindow("Ошибка", errors.ToString()).ShowDialog();
                return;
            }

            if (_current.ID == 0)
                CarResaleEntities.GetContext().Customers.Add(_current);

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
