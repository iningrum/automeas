using automeas_ui._Launcher.Model;
using automeas_ui._MVG.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._MVG.ViewModel
{
    public partial class HomeViewModel : IViewMVG
    {
        public HomeViewModel()
        {
        }
        [RelayCommand]
        void SaveMVGData()
        {
            throw new NotImplementedException();
        }
        [RelayCommand]
        void PushCreator()
        {
            var master = (MainViewModel)automeas_ui._Common.Navigator.MVG._handle;
            master.View.Page = 1;
            master.View.Current = automeas_ui._Common.Navigator.MVG.Change<IViewMVG>("Push");
        }
        [RelayCommand]
        void PullCreator()
        {
            var master = (MainViewModel)automeas_ui._Common.Navigator.MVG._handle;
            master.View.Page = 2;
            master.View.Current = automeas_ui._Common.Navigator.MVG.Change<IViewMVG>("Push");
        }

    }
}
