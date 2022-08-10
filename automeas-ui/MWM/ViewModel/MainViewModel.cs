using automeas_ui.Core;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }

        private JSRelayCommand pushMoveCreator;
        public ICommand PushMoveCreator => pushMoveCreator ??= new JSRelayCommand(PerformPushMoveCreator);

        private void PerformPushMoveCreator(object commandParameter)
        {
        }
    }
}
