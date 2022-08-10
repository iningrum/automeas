using automeas_ui.MWM.Model.Launcher;
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
            Navigator.Instance.ChangeWindow("\0"); // shutdown app
        }
    }
}
