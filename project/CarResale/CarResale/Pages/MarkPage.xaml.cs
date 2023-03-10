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
using CarResale.DBModel;
using CarResale.Windows;

namespace CarResale.Pages
{
    public partial class MarkPage : Page
    {
        public MarkPage()
        {
            InitializeComponent();

            SetDefaultItemSource();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new MarkAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new MarkAddPage(DG.SelectedItem as Mark)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaultItemSource(); };

            for (char symbol = 'A'; symbol <= 'Z'; symbol++)
            {
                FirstSymbolCB.Items.Add(symbol);
            }

            FirstSymbolCB.SelectionChanged += (s, e) => { SelectedSymbol(); };

        }

        private void SetDefaultItemSource()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
        }

        private void SelectedSymbol()
        {
            char selectedItem = FirstSymbolCB.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()[0];
            DG.ItemsSource = CarResaleEntities.GetContext().Marks.ToList().Where(x => x.Name[0] == selectedItem);
        }

        private void Delete()
        {
            if (!(bool)new QuestionWindow("Внимание!", "Вы точно хотите удалить выбранные элементы?").ShowDialog()) return;

            var selectedItems = DG.SelectedItems.Cast<Mark>().ToList();

            try
            {
                CarResaleEntities.GetContext().Marks.RemoveRange(selectedItems);
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
