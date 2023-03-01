using CarResale.DBModel;
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
    /// <summary>
    /// Логика взаимодействия для ModelAddPage.xaml
    /// </summary>
    public partial class ModelAddPage : Page
    {
        private Model _current = new Model();
        public ModelAddPage(Model selected = null)
        {
            InitializeComponent();

            if (selected != null)
            {
                _current = selected;
            }

            DataContext = _current;
            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();

            SaveBtn.Click += (s, e) => { SaveData(); };
            CancelBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelPage()); };

        }
        private void SaveData()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_current.Model1))
                errors.AppendLine("Введите название марки автомобиля");
            if(_current.Mark == null)
                errors.AppendLine("Выберите марку автомобиля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current.ID == 0)
                CarResaleEntities.GetContext().Models.Add(_current);

            try
            {
                CarResaleEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                CancelBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
