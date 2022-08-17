using automeas_ui._Launcher.Model;
using automeas_ui._Launcher.ViewModel;
using System.Windows;

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
            Navigator.Instance.ChangeWindow("\0"); // shutdown app
        }
    }
}
