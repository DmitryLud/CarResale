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
using CarResale.DBModel;
using CarResale.Windows;

namespace CarResale.Pages
{
    /// <summary>
    /// Логика взаимодействия для MarkAddPage.xaml
    /// </summary>
    public partial class MarkAddPage : Page
    {
        private Mark _current = new Mark();
        public MarkAddPage(Mark selected = null)
        {
            InitializeComponent();

            if(selected != null)
            {
                _current = selected;
            }

            DataContext = _current;

            SaveBtn.Click += (s, e) => { SaveData(); };
            CancelBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new MarkPage()); };

        }

        private void SaveData()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_current.Name))
                errors.AppendLine("Введите название марки автомобиля");

            if (errors.Length > 0)
            {
                new InfoWindow("Ошибка", errors.ToString()).ShowDialog();
                return;
            }

            if (_current.ID == 0)
                CarResaleEntities.GetContext().Marks.Add(_current);

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
