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
using automeas_ui.MWM.ViewModel;
namespace automeas_ui
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

        private void NextPage(object sender, RoutedEventArgs e)
        {
            PageBarViewModel.NextPage();
        }
    }
}
