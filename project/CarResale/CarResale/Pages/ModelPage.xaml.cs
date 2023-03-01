using CarResale.DBModel;
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
    /// <summary>
    /// Логика взаимодействия для ModelPage.xaml
    /// </summary>
    public partial class ModelPage : Page
    {
        public ModelPage()
        {
            InitializeComponent();
            SetDefaultItemSource();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage(DG.SelectedItem as Model)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaultItemSource(); };

            for (char symbol = 'A'; symbol <= 'Z'; symbol++)
            {
                FirstSymbolCB.Items.Add(symbol);
            }
            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();

            FirstSymbolCB.SelectionChanged += (s, e) => { SelectedSymbol(); };
            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };
        }
        private void SetDefaultItemSource()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();
        }

        private void SelectedSymbol()
        {
            char selectedItem = FirstSymbolCB.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()[0];
            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList().Where(x => x.Model1[0] == selectedItem);
        }

        private void SelectedMark()
        {
            var selectedItem = CarResaleEntities.GetContext().Marks.ToList().Where(x=> x.Mark1 == MarkCB.Text).ToList()[0].ID;
            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList().Where(x => x.MarkID == selectedItem);
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить выбранные элементы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            var selectedItems = DG.SelectedItems.Cast<Mark>().ToList();

            try
            {
                CarResaleEntities.GetContext().Marks.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().SaveChanges();
                DG.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
                MessageBox.Show("Данные успешно удалены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
    }
}
