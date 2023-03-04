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
    public partial class CarAddPage : Page
    {
        private Cars _current = new Cars();
        public CarAddPage(Cars selected = null)
        {

            InitializeComponent();

            if (selected != null) _current = selected;
            DataContext = _current;

            ModelCB.IsEnabled = false;

            MarkCB.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();
            MarkCB.SelectionChanged += (s, e) => { SelectedMark(); };

            SaveBtn.Click += (s, e) => { SaveData(); };

            AcquisistionPriceTB.LostFocus += (s, e) => { SetTotalAcquisistionPrice(); };
            OtherCostsTB.LostFocus += (s, e) => { SetTotalAcquisistionPrice(); };

        }

        private void SetTotalAcquisistionPrice()
        {
            if (string.IsNullOrWhiteSpace(AcquisistionPriceTB.Text) ||
                string.IsNullOrWhiteSpace(OtherCostsTB.Text)) return;
            TotalAcquisistionPriceTB.Text = (int.Parse(AcquisistionPriceTB.Text) + int.Parse(OtherCostsTB.Text)).ToString();
        }

        private void SelectedMark()
        {
            if (MarkCB.SelectedValue == null)
            {
                ModelCB.IsEnabled = false;
                return;
            }

            int selectedItem = (MarkCB.SelectedItem as Marks).ID;
            ModelCB.IsEnabled = true;
            ModelCB.ItemsSource = CarResaleEntities.GetContext().GetModels(selectedItem).ToList();
        }

        private void SaveData()
        {
            StringBuilder errors = new StringBuilder();
            if (_current.Models == null)
                errors.AppendLine("Выберите модель");
            if (_current.Models.Marks == null)
                errors.AppendLine("Выберите марку автомобиля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_current.ID == 0)
                CarResaleEntities.GetContext().Cars.Add(_current);

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
