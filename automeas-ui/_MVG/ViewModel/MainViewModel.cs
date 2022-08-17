using automeas_ui._Launcher.Model;
using automeas_ui._MVG.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._MVG.ViewModel
{
    [ObservableObject]
    public partial class MVGMainViewModelBindings
    {
        [ObservableProperty]
        private int page = 0;
        [ObservableProperty]
        private IViewMVG current;
    }
    public partial class MainViewModel
    {
        public MainViewModel()
        {
            View = new();
            View.Page = 0;
            View.Current = automeas_ui._Common.Navigator.MVG.GetCurrent<IViewMVG>();
        }
        public MVGMainViewModelBindings View { get; set; }
    }
}
