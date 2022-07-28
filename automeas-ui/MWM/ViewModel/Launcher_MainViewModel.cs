using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using automeas_ui.MWM.ViewModel.Launcher.Pages;
using System;
using System.Windows.Input;

/*
         * Launcher_MainViewModel is the main view that manages navigation between other views
         * ----------------------------------------------------------------
         * 
         */
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
        public Target Config { get; set; }

        // events
        public event Action<int>? PageChanged; // sent to PageBarView
        public event Action<int>? PageNoLongerRelevant; // sent to subviews
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
        public void _PageBarView_PageChanged(int sender)
        {
            //CurrentView.Value = GetCurrentView(sender);
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
                case 2:
                    {
                        var result = new UploadConfigFileViewModel();
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
