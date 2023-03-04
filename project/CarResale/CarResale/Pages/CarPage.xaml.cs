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
    public partial class CarPage : Page
    {
        public CarPage()
        {
            InitializeComponent();

            ModelCB.IsEnabled = false;

            DG.ItemsSource = CarResaleEntities.GetContext().Cars.ToList();

            AddBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new CarAddPage()); };
            ChangeBtn.Click += (s, e) => { Manager.MainFrame.Navigate(new CarAddPage(DG.SelectedItem as Cars)); };
            DeleteBtn.Click += (s, e) => { Delete(); };
            ClearBtn.Click += (s, e) => { SetDefaulFilter(); };
            SearchBtn.Click += (s, e) => { Search(); };
            
            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
            ColorCB.ItemsSource = CarResaleEntities.GetContext().CarColors.ToList();
            TrimCB.ItemsSource = CarResaleEntities.GetContext().CarTrims.ToList();

            TransmissionCB.Items.Add("Автоматическая");
            TransmissionCB.Items.Add("Механическая");
            TransmissionCB.Items.Add("Роботизированная");
            TransmissionCB.Items.Add("Вариативная");
            

            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };
            ModelCB.SelectionChanged += (s, e) => { SelectedModel(); };
            TransmissionCB.SelectionChanged += (s, e) => { SelectedTransmission(); };


        }
        private void SetDefaulFilter()
        {
            DG.ItemsSource = CarResaleEntities.GetContext().Cars.ToList();

            SearchTB.Text = null;
            MarkCB.Text = null;
            ModelCB.Text = null;
            TransmissionCB.Text = null;
            ColorCB.Text = null;
            MileageCB.Text = null;
            TrimCB.Text = null;
            YearCB.Text = null;

        }

        //private void SelectedSymbol()
        //{
        //    if (FirstSymbolCB.SelectedValue == null) return;
        //    char selectedItem = FirstSymbolCB.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last()[0];
        //    DG.ItemsSource = (DG.ItemsSource as List<Model>).Where(x => x.Model1[0] == selectedItem).ToList();
        //}

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null)
            {
                ModelCB.IsEnabled = false;
                return;
            }

            int selectedItem = (MarkCB.SelectedItem as Marks).ID;
            DG.ItemsSource = (DG.ItemsSource as List<Cars>).Where(x => x.Models.MarkID == selectedItem).ToList();

            ModelCB.IsEnabled = true;
            ModelCB.ItemsSource = CarResaleEntities.GetContext().GetModels(selectedItem).ToList();
        }

        private void SelectedModel()
        {
            if (ModelCB.SelectedValue == null) return;
            string selectedItem = ModelCB.Text;
            DG.ItemsSource = (DG.ItemsSource as List<Cars>).Where(x => x.Models.Model == selectedItem).ToList();
        }

        private void SelectedTransmission()
        {
            if (TransmissionCB.Text == null) return;
            string selectedItem = TransmissionCB.Text;
            DG.ItemsSource = (DG.ItemsSource as List<Cars>).Where(x => x.Transmission == selectedItem).ToList();
        }

        private void Search()
        {
            DG.ItemsSource = (DG.ItemsSource as List<Models>).Where(x => x.Model.Contains(SearchTB.Text)).ToList();
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить выбранные элементы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            var selectedItems = DG.SelectedItems.Cast<Marks>().ToList();

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
