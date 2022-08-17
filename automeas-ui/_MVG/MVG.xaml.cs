using automeas_ui._Common;
using automeas_ui._MVG.Model;
using automeas_ui._MVG.ViewModel;
using automeas_ui.MVGenerator.MVVM.ViewModel;
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
using MoveCreatorViewModel = automeas_ui._MVG.ViewModel.MoveCreatorViewModel;

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
            App app = (App)Navigator.App._handle;
            automeas_ui._MVG.ViewModel.MainViewModel master = (automeas_ui._MVG.ViewModel.MainViewModel)Navigator.MVG._handle;
            if (master.View.Page != 0)
            {
                master.View.Page = 0;
                master.View.Current = Navigator.MVG.Change<IViewMVG>("\r");
            }
            else
            {
                app.sSwitch("\r");
            }
            
        }
    }
}
