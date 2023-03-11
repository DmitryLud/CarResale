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
    public partial class ModelPage : Page
    {
        public ModelPage()
        {
            InitializeComponent();

            DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new ModelAddPage(DG.SelectedItem as Model)); };
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
            DG.ItemsSource = (DG.ItemsSource as List<Model>).Where(x => x.Name[0] == selectedItem).ToList();
        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null) return;
            int selectedItem = (MarkCB.SelectedItem as Mark).ID;
            DG.ItemsSource = (DG.ItemsSource as List<Model>).Where(x => x.MarkID == selectedItem).ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Model>).Where(x=>x.Name.Contains(SearchTB.Text) ||
                x.Mark.Name.Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (!(bool)new QuestionWindow("Внимание!", "Вы точно хотите удалить выбранные элементы?").ShowDialog()) return;

            var selectedItems = DG.SelectedItems.Cast<Model>().ToList();

            try
            {
                CarResaleEntities.GetContext().Models.RemoveRange(selectedItems);
                CarResaleEntities.GetContext().SaveChanges();
                DG.ItemsSource = CarResaleEntities.GetContext().Models.ToList();
                new InfoWindow("Уведомление", "Данные успешно удалены!").ShowDialog();
            }
            catch (Exception ex)
            {
                new InfoWindow("Ошибка", ex.Message).ShowDialog();
            }

        }
    }
}
