using automeas_ui._Launcher.Model;
using CommunityToolkit.Mvvm.Input;
using automeas_ui._Common;
using Target = automeas_ui._Launcher.Model.Target;
using Navigator = automeas_ui._Common.Navigator;

namespace automeas_ui._Launcher.ViewModel.Pages
{
    public partial class UploadConfigFileViewModel : ILauncherPage
    {
        // ctor
        public UploadConfigFileViewModel()
        {
            NOfRepeatsFontSize = new ObservableType<int>(32);
            NOfRepeatsInt = new ObservableType<int>(Target.Instance.NumberOfMoves);
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
        void OpenMVG() => ((App)Navigator.App._handle).sSwitch("MVG");
        public void RefreshIntegerUpDown(int msg) => Target.Instance.NumberOfMoves = msg;

        public void Save()
        {
            HandlePageChanged(ID);
        }
    }
}
