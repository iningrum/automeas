using automeas_ui._Launcher.Model;
using automeas_ui._Launcher.ViewModel.Pages;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target = automeas_ui._Launcher.Model.Target;

namespace automeas_ui._Launcher.ViewModel
{
    public partial class Launcher_MainViewModel
    {
        // new
        public ObservableType<object> CurrentView { get; set; }
        // end
        public Launcher_MainViewModel()
        {
            Target.Instance.Launcher_MainViewModel_SetMaster(this);
            CurrentView = new ObservableType<object>(null);
            GetCurrentPage(0);
            // views
            PageBarView = new PageBarViewModel(this);
            // event links
            PageBarView.PageChanged += _PageBarView_PageChanged;
            // observables
            CurrentPage = new ObservableType<int>(0);
            CurrentPageTitle = new ObservableType<string>(AMDevConfig.PageTitles[CurrentPage.Value]);
        }
        public PageBarViewModel PageBarView { get; set; }
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<string> CurrentPageTitle { get; set; }

        // events
        public event Action<int>? PageChanged; // sent to PageBarView
        public event Action<int>? PageNoLongerRelevant; // sent to subviews
        [RelayCommand]
        void NextPage() => PageChanged?.Invoke(CurrentPage.Value + 1);
        [RelayCommand]
        void PreviousPage() => PageChanged?.Invoke(CurrentPage.Value - 1);
        // handlers

        public void _PageBarView_PageChanged(int sender)
        {
            PageNoLongerRelevant?.Invoke(CurrentPage.Value);
            CurrentPage.Value = sender;
            GetCurrentPage(sender);
            CurrentPageTitle.Value = AMDevConfig.PageTitles[sender];
            return;
        }
        private void GetCurrentPage(int cp)
        {
            switch (cp)
            {
                case 0:
                    {
                        var result = new Page1();
                        CurrentView.Value = result;
                    }
                    break;
                case 1:
                    {
                        var result = new NameDescriptionViewModel();
                        CurrentView.Value = result;
                    }
                    break;
                case 2:
                    {
                        var result = new UploadConfigFileViewModel();
                        CurrentView.Value = result;
                    }
                    break;
                case 3:
                    {
                        var result = new LauncherSummaryViewModel();
                        CurrentView.Value = result;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
