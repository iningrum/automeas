using automeas_ui.Core;
using automeas_ui.MWM.Model;
using System;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    public class Launcher_MainViewModel
    {
        public Launcher_MainViewModel()
        {
            PageBarView = new PageBarViewModel(this);
            PageBarView.PageChanged += _PageBarView_PageChanged;
            // observables
            CurrentPage = new ObservableType<int>(0);
            CurrentPageTitle = new ObservableType<string>(PageTitles.Get(CurrentPage.Value));
        }
        private object _currentView; // responsible for switching views
        public PageBarViewModel PageBarView { get; set; }
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<string> CurrentPageTitle { get; set; }

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
