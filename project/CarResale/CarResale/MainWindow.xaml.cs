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

namespace CarResale
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CarsBtn.Click += (s, e) => SetPage(new Pages.CarPage(), "Cars");
            MarksBtn.Click += (s, e) => SetPage(new Pages.MarkPage(), "Marks");
            ModelsBtn.Click += (s, e) => SetPage(new Pages.ModelPage(), "Models");
            ClientsBtn.Click += (s, e) => SetPage(new Pages.ClientPage(), "Clients");
            OrdersBtn.Click += (s, e) => SetPage(new Pages.OrderPage(), "Orders");
            ReceiptInvoiceBtn.Click += (s, e) => SetPage(new Pages.ReceiptInvoicePage(), "Receipt Invoice");
            //ReportsBtn.Click += (s, e) => SetPage(new Pages.(), "Reports");

            ThemeBtn.Click += (s, e) =>
            {
                ((App)Application.Current).ChangeTheme();
            };
            Manager.MainFrame = MainFrame;
        }

        public void SetPage(Page newPage, string pageName)
        {
            MainFrame.Navigate(newPage);
            CurrentPageName.Text = pageName;
        }

    }
}
