using automeas_ui._Launcher.Model;
using automeas_ui._Launcher.ViewModel;
using System.Windows;
using automeas_ui._Common;

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
            this.DataContext = new Launcher_MainViewModel();
        }
        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            Navigator.Launcher.Change("\0"); // shutdown app
        }
    }
}
