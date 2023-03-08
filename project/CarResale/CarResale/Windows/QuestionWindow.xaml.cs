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
using System.Windows.Shapes;

namespace CarResale.Windows
{
    public partial class QuestionWindow : Window
    {
        public QuestionWindow(string title, string question)
        {
            InitializeComponent();
            Title.Content = title;
            TBQuestion.Text = question;

            ButtonNo.Click += (s, e) => { DialogResult = false; };
            ButtonYes.Click += (s, e) => { DialogResult = true; };
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
