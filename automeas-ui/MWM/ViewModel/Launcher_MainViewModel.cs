using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using automeas_ui.MWM.ViewModel.Launcher.Pages;
using System;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    public class Launcher_MainViewModel
    {
        // new
        public ObservableType<object> CurrentView { get; set; }
        // end
        public Launcher_MainViewModel()
        {
            Config = new Target(this);
            CurrentView = new ObservableType<object>(null);
            GetCurrentPage(0);

            
            // pages
            // views
            PageBarView = new PageBarViewModel(this);
            // event links
            PageBarView.PageChanged += _PageBarView_PageChanged;
            // observables
            CurrentPage = new ObservableType<int>(0);
            CurrentPageTitle = new ObservableType<string>(PageTitles.Get(CurrentPage.Value));
        }
        //private object _currentView; // responsible for switching views
        Page1 p1;
        NameDescriptionViewModel p2;
        public PageBarViewModel PageBarView { get; set; }
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<string> CurrentPageTitle { get; set; }
        public Target Config { get; set; }

        // events
        public event Action<int>? PageChanged;
        public event Action<int>? PageNoLongerRelevant;
        public event Action? NameDescriptionOutOfFocus;
        void NextPage() => PageChanged?.Invoke(CurrentPage.Value + 1);
        void PreviousPage() => PageChanged?.Invoke(CurrentPage.Value - 1);
        // handlers
        private ICommand? _npCommand;
        private ICommand? _ppCommand;
        public ICommand NpCommad
        {
            get
            {
                if (_npCommand == null)
                {
                    _npCommand = new JSRelayCommand(
                        param => this.NextPage()
                    );
                }
                return _npCommand;
            }
        }
        public ICommand PpCommad
        {
            get
            {
                if (_ppCommand == null)
                {
                    _ppCommand = new JSRelayCommand(
                        param => this.PreviousPage()
                    );
                }
                return _ppCommand;
            }
        }
        private void _PageBarView_PageChanged(int sender)
        {
            //CurrentView.Value = GetCurrentView(sender);
            GetCurrentPage(sender);
            PageNoLongerRelevant?.Invoke(CurrentPage.Value);
            CurrentPage.Value = sender;
            
            CurrentPageTitle.Value = PageTitles.Get(sender);
            return;
        }
        private void GetCurrentPage(int cp)
        {
            switch (cp)
            {
                case 0:
                    {
                        var result = new Page1();
                        result.Load(Config);
                        CurrentView.Value = result;
                    }
                    break;
                case 1:
                    {
                        var result = new NameDescriptionViewModel();
                        result.Load(Config);
                        CurrentView.Value = result;
                    }
                    break;
                case 3:
                    {
                        var result = new LauncherSummaryViewModel(Config);
                        CurrentView.Value = result;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
