using automeas_ui._Launcher.Model;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigator = automeas_ui._Launcher.Model.Navigator;
using Target = automeas_ui._Launcher.Model.Target;

namespace automeas_ui._Launcher.ViewModel.Pages
{
    public partial class UploadConfigFileViewModel : ViewPage
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

        public void HandlePageChanged(int msg)
        {
            if (msg != ID)
                return;
            Target.Instance.NumberOfMoves = NOfRepeatsInt.Value;
        }
        // handle drag&drop
        public void DragDropFile(string filename, string path)
        {
            Target.Instance.ConfigFileName = filename;
            Target.Instance.ConfigFilePath = path;
            return;
        }
        // icommand
        [RelayCommand]
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
        [RelayCommand]
        void OpenMVG() => Navigator.Instance.ChangeWindow("mvg");
        public void RefreshIntegerUpDown(int msg) => Target.Instance.NumberOfMoves = msg;

        public override void Save()
        {
            HandlePageChanged(ID);
        }
    }
}
