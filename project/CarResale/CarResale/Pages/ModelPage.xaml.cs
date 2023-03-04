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
    public partial class ModelPage : Page
    {
        public ModelPage()
        {
            InitializeComponent();

            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage(DG.SelectedItem as Models)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaulFilter(); };
            SearchBtn.Click += (s, e) => { Search(); };

            for (char symbol = 'A'; symbol <= 'Z'; symbol++)
            {
                FirstSymbolCB.Items.Add(symbol);
            }
            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();

            FirstSymbolCB.SelectionChanged += (s, e) => { SelectedSymbol(); };
            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };
        }
        private void SetDefaulFilter()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();

            SearchTB.Text = null;
            FirstSymbolCB.Text = null;
            MarkCB.Text = null;

        }

        private void SelectedSymbol()
        {
            if(FirstSymbolCB.SelectedValue == null) return;
            char selectedItem = FirstSymbolCB.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()[0];
            DG.ItemsSource = (DG.ItemsSource as List<Models>).Where(x => x.Model[0] == selectedItem).ToList();
        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null) return;
            int selectedItem = (MarkCB.SelectedItem as Marks).ID;
            DG.ItemsSource = (DG.ItemsSource as List<Models>).Where(x => x.MarkID == selectedItem).ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Models>).Where(x=>x.Model.Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить выбранные элементы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            var selectedItems = DG.SelectedItems.Cast<Models>().ToList();

            try
            {
                CarResaleEntities.GetContext().Models.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().SaveChanges();
                DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();
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
