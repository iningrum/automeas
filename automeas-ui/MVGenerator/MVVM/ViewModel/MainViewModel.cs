using automeas_ui.Core;
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
        private ICommand? _pushMoveCreator;
        private ICommand? _pullMoveCreator;
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
        public void OpenPullCreator()
        {

        }
        public void OpenPushCreator()
        {

        }
    }
    
}
