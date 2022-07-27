using automeas_ui.Core;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class UploadConfigFileViewModel : BaseViewModel, IBaseViewModel
    {
        // ctor
        public UploadConfigFileViewModel()
        {

        }
        // attr
        private int ID = 2;
        // IBaseViewModel
        public void Bind(Target T, Action<int> handler)
        {
            _target = T;
            _target.PageChangedEvent += handler;
        }

        public void HandlePageChanged(int msg)
        {
            if (msg != ID)
                return;

        }

        public void Load(Target T)
        {
            Bind(T, HandlePageChanged);
        }
        // handle drag&drop
        public void DragDropFile(string filename, string path)
        {
            _target.ConfigFileName = filename;
            _target.ConfigFilePath = path;
            return;
        }
        // icommand
        private ICommand? _ChooseFileCommand;
        public ICommand ChooseFileCommand
        {
            get
            {
                if (_ChooseFileCommand == null)
                {
                    _ChooseFileCommand = new JSRelayCommand(
                        param => this.ChooseFile()
                    );
                }
                return _ChooseFileCommand;
            }
        }
        // command
        void ChooseFile()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                string src = openFileDlg.SelectedPath;
                if (src == null || src.Length == 0)
                {
                    return;
                }
                _target.ConfigFileName = System.IO.Path.GetFileName(src);
                _target.ConfigFilePath = src;
            }
        }
    }
}
