using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarResale
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _darkTheme = true;
        private ResourceDictionary ThemeDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
        }
        public void ChangeTheme()
        {
            if (_darkTheme)
            {
                _darkTheme = false;
                ThemeDictionary.MergedDictionaries.Clear();
                ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Themes/LightTheme.xaml") });
                return;
            }

            _darkTheme = true;
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/Themes/DarkTheme.xaml") });

        }
    }
}
