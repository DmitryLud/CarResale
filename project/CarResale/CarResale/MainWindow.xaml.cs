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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CarsBtn.Click += (s, e) => SetPage(new Pages.CarPage(), "Автомобили");
            MarksBtn.Click += (s, e) => SetPage(new Pages.MarkPage(), "Марки");
            ModelsBtn.Click += (s, e) => SetPage(new Pages.ModelPage(), "Модели");
            ClientsBtn.Click += (s, e) => SetPage(new Pages.ClientPage(), "Клиенты");
            OrdersBtn.Click += (s, e) => SetPage(new Pages.OrderPage(), "Заказы");
            ReportsBtn.Click += (s, e) => SetPage(new Pages.ReportPage(), "Отчеты");

            Manager.MainFrame = MainFrame;
            SetPage(new Pages.OrderPage(), "Заказы");
        }

        public void SetPage(Page newPage, string pageName)
        {
            MainFrame.Navigate(newPage);
            CurrentPageName.Text = pageName;
        }

    }
}
