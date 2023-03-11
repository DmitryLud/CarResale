using CarResale.DBModel;
using CarResale.Windows;
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
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            SetDefaultItemSource();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ClientAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ClientAddPage(DG.SelectedItem as Customer)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            SearchBtn.Click += (s, e) => { Search(); };


        }

        private void SetDefaultItemSource()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Customers.ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Customer>).Where(x => x.Name.Contains(SearchTB.Text) ||
                x.Surname.Contains(SearchTB.Text) ||
                x.Phone.Contains(SearchTB.Text) ||
                x.Email.Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (!(bool)new QuestionWindow("Внимание!", "Вы точно хотите удалить выбранные элементы?").ShowDialog()) return;

            var selectedItems = DG.SelectedItems.Cast<Customer>().ToList();

            try
            {
                CarResaleEntities.GetContext().Customers.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().SaveChanges();
                DG.Items.Remove(selectedItems);
                new InfoWindow("Уведомление", "Данные успешно удалены!").ShowDialog();
            }
            catch (Exception ex)
            {
                new InfoWindow("Ошибка", ex.Message).ShowDialog();
            }

        }
    }
}
