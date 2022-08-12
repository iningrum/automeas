using automeas_ui.MVGenerator.MVVM.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
