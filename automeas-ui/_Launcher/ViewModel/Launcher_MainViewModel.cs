using automeas_ui._Launcher.Model;
using automeas_ui._Launcher.ViewModel.Pages;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target = automeas_ui._Launcher.Model.Target;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;
using automeas_ui._Common;
using Navigator = automeas_ui._Common.Navigator;

namespace automeas_ui._Launcher.ViewModel
{

    [ObservableObject]
    public partial class CurrentView
    {
        [ObservableProperty]
        private object current;
        [ObservableProperty]
        private int page;
        [ObservableProperty]
        private string title;
    }
    public partial class Launcher_MainViewModel
    {
        // new
        public CurrentView View { get; set; }
        // end
        public Launcher_MainViewModel()
        {
            Target.Instance.Launcher_MainViewModel_SetMaster(this);
            View = new();
            View.Current = Navigator.Launcher.Change<object>("0");
            //GetCurrentPage(0);
            // views
            PageBarView = new PageBarViewModel(this);
            // event links
            PageBarView.PageChanged += _PageBarView_PageChanged;
            // observables
            View.Page = 0;
            View.Title = DevConfig.PageTitles[View.Page];
        }
        public PageBarViewModel PageBarView { get; set; }
        //public ObservableType<int> CurrentPage { get; set; }
        //public ObservableType<string> CurrentPageTitle { get; set; }

        // events
        public event Action<int>? PageChanged; // sent to PageBarView
        public event Action<int>? PageNoLongerRelevant; // sent to subviews
        [RelayCommand]
        void NextPage() => PageChanged?.Invoke(View.Page + 1);
        [RelayCommand]
        void PreviousPage() => PageChanged?.Invoke(View.Page - 1);
        // handlers

        public void _PageBarView_PageChanged(int sender)
        {
            PageNoLongerRelevant?.Invoke(View.Page);
            View.Page = sender;
            GetCurrentPage(sender);
            View.Title = DevConfig.PageTitles[sender];
            return;
        }
        private void GetCurrentPage(int cp)
        {
            /*switch (cp)
            {
                case 0:
                    {
                        var result = new Page1();
                        View.Current = result;
                    }
                    break;
                case 1:
                    {
                        var result = new NameDescriptionViewModel();
                        View.Current = result;
                    }
                    break;
                case 2:
                    {
                        var result = new UploadConfigFileViewModel();
                        View.Current = result;
                    }
                    break;
                case 3:
                    {
                        var result = new LauncherSummaryViewModel();
                        View.Current = result;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }*/
            View.Current = Navigator.Launcher.Change<object>(cp.ToString());

        }
    }
}
