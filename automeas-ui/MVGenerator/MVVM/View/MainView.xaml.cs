using automeas_ui.MVGenerator.MVVM.Model;
using System.Windows;
using System.Windows.Controls;

namespace automeas_ui.MVGenerator.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void PushButton_Click(object sender, RoutedEventArgs e)
        {
            MVGTarget.Instance.LoadCurrentMove(0);
            MVGTarget.Instance.NotifyViewNavigate("push");
        }
        private void PullButton_Click(object sender, RoutedEventArgs e)
        {
            MVGTarget.Instance.LoadCurrentMove(1);
            MVGTarget.Instance.NotifyViewNavigate("pull");
        }
    }
}
