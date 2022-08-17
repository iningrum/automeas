using automeas_ui._Common;
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

namespace automeas_ui._MVG
{
    /// <summary>
    /// Logika interakcji dla klasy MVG.xaml
    /// </summary>
    public partial class MVG : Window
    {
        public MVG()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((App)Navigator.App._handle).sSwitch("\r");
        }
    }
}
