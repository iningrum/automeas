using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal class MainViewModel
    {
        // ctor
        public MainViewModel()
        {
            CurrentPage = new ObservableType<int>(0);
            CurrentView = new ObservableType<object>(this);
        }
        // attr
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<object> CurrentView { get; set; }
        // cmd
        private ICommand? _pushMoveCreator;
        private ICommand? _pullMoveCreator;
        private ICommand? _saveData;
        private ICommand? _goToMainView;
        public ICommand GoToMainView
        {
            get
            {
                if(_goToMainView == null)
                {
                    _goToMainView = new JSRelayCommand(
                        param => this.ReturnToMainView()
                    );
                }
                return _goToMainView;
            }
        }
        public ICommand PushMoveCreator
        {
            get
            {
                if (_pushMoveCreator == null)
                {
                    _pushMoveCreator = new JSRelayCommand(
                        param => this.OpenPushCreator()
                    );
                }
                return _pushMoveCreator;
            }
        }
        public ICommand PullMoveCreator
        {
            get
            {
                if (_pullMoveCreator == null)
                {
                    _pullMoveCreator = new JSRelayCommand(
                        param => this.OpenPullCreator()
                    );
                }
                return _pullMoveCreator;
            }
        }
        public ICommand SaveData
        {
            get
            {
                if (_saveData == null)
                {
                    _saveData = new JSRelayCommand(
                        param => this.SaveMVGData()
                    );
                }
                return _saveData;
            }
        }
        public void SaveMVGData()
        {
            // open file browser, save data, generate data
            Navigator.Instance.ChangeWindow("\r");
        }
        public void OpenPullCreator()
        {
            CurrentPage.Value = 2;
            CurrentView.Value = new MoveCreatorViewModel(false);
        }
        public void OpenPushCreator()
        {
            CurrentPage.Value = 1;
            CurrentView.Value = new MoveCreatorViewModel(true);
        }
        public void ReturnToMainView()
        {
            CurrentPage.Value = 0;
            CurrentView.Value = this;
        }
    }
    
}
