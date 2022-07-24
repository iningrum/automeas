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
        public Launcher_MainViewModel()
        {
            // pages
            p1 = new Page1(this);
            // views
            PageBarView = new PageBarViewModel(this);
            CurrentView = p1;
            // event links
            PageBarView.PageChanged += _PageBarView_PageChanged;
            // observables
            CurrentPage = new ObservableType<int>(0);
            CurrentPageTitle = new ObservableType<string>(PageTitles.Get(CurrentPage.Value));
            Config = new Target(p1);
        }
        private readonly Page1 p1;
        private object _currentView; // responsible for switching views
        public PageBarViewModel PageBarView { get; set; }
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<string> CurrentPageTitle { get; set; }
        public Target Config { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
            }
        }
        // events
        public event Action<int>? PageChanged;
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
            CurrentPage.Value = sender;
            CurrentPageTitle.Value = PageTitles.Get(sender);
            return;
        }
    }
}
