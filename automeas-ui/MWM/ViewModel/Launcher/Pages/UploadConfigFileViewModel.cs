using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Windows.Input;

/*
         * Handles, loads and verifies integrity of Move file
         * ----------------------------------------------------------------
         */
namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class UploadConfigFileViewModel : BaseViewModel, IBaseViewModel
    {
        // ctor
        public UploadConfigFileViewModel()
        {
            { // load Target
                Target.Instance.PageChangedEvent += HandlePageChanged;
                NOfRepeatsInt = new ObservableType<int>(Target.Instance.NumberOfMoves);
            }
            NOfRepeatsFontSize = new ObservableType<int>(32);
            NOfRepeatsInt = new ObservableType<int>(1);
        }
        // attr
        private int ID = 2;
        public ObservableType<int> NOfRepeatsFontSize { get; set; }
        public ObservableType<int> NOfRepeatsInt { get; set; }
        // IBaseViewModel
        /*public void Bind(Target T, Action<int> handler)
        {
            Target.Instance = T;
            Target.Instance.PageChangedEvent += handler;
        }*/

        public void HandlePageChanged(int msg)
        {
            if (msg != ID)
                return;
            Target.Instance.NumberOfMoves = NOfRepeatsInt.Value;
        }

        /*public void Load(Target T)
        {
            Bind(T, HandlePageChanged);
            NOfRepeatsInt.Value = Target.Instance.NumberOfMoves;
        }*/
        // handle drag&drop
        public void DragDropFile(string filename, string path)
        {
            Target.Instance.ConfigFileName = filename;
            Target.Instance.ConfigFilePath = path;
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
                Target.Instance.ConfigFileName = System.IO.Path.GetFileName(src);
                Target.Instance.ConfigFilePath = src;
            }
        }
        public void RefreshIntegerUpDown(int msg) => Target.Instance.NumberOfMoves = msg;
    }
}
