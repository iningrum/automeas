using automeas_ui.MWM.Model.Launcher;
using System.Windows;

namespace automeas_ui
{
    /// <summary>
    /// Logika interakcji dla Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            Navigator.Instance.ChangeWindow("\r");
        }
    }

}
