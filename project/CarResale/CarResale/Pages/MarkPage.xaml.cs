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

namespace CarResale.Pages
{
    /// <summary>
    /// Логика взаимодействия для MarkPage.xaml
    /// </summary>
    public partial class MarkPage : Page
    {
        public MarkPage()
        {
            InitializeComponent();

            DG.ItemsSource = CarResaleEntities.GetContext().Marks.ToList();

        }
    }
}
