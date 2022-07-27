using automeas_ui.Core;
using System.Windows;
namespace automeas_ui
{
    public partial class Launcher : Window
    {
        public Launcher()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
