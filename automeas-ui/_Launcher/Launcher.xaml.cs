using automeas_ui._Launcher.Model;
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

namespace automeas_ui._Launcher
{
    /// <summary>
    /// Logika interakcji dla klasy Launcher.xaml
    /// </summary>
    public partial class Launcher : Window
    {
        public Launcher()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            Navigator.Instance.ChangeWindow("\0"); // shutdown app
        }
    }
}
