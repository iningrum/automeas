using automeas_ui.Core;
using automeas_ui.MWM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class CheckBox
    {
        public CheckBox(string text = "Sample text", bool IsActive = false, bool IsEditable = true)
        {
            Enabled = IsEditable;
            Checked = IsActive;
            Text = text;
        }
        public bool Enabled { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }
    }
    public class Page1
    {
        // internal interface
        const int NumberOfPages = 5;
        private string[] CheckBoxText = 
        {
            "*.csv",
            "*.pdf (template required)",
            "*.docx (template required)",
            "*.xslx (template required)",
            "*.jpg (templates required)"
        };
        // ctor
        public Page1(Launcher_MainViewModel master)
        {
            this.master = master;
            ChosenTargetPath = new ObservableType<string>("File Path     📁  ");
            //master.PageChanged += LauncherMaster_PageChanged;
            Options = new TrulyObservableCollection<ObservableType<CheckBox>>();
            Options.Add(new ObservableType<CheckBox>(new CheckBox(CheckBoxText[0], true, false)));
            for (int i = 1; i < NumberOfPages; i++)
            {
                Options.Add(new ObservableType<CheckBox>(new CheckBox(CheckBoxText[i])));
            }
        }
        // attrs
        public ObservableType<string> ChosenTargetPath { get; set; }
        private readonly Launcher_MainViewModel master;
        private TrulyObservableCollection<ObservableType<CheckBox>> _Options;
        public TrulyObservableCollection<ObservableType<CheckBox>> Options
        {
            get { return _Options; }
            set
            {
                if (_Options == value) return;
                _Options = value;
                NotifyPropertyChanged();
                NotifyOptionsChanged();
            }
        }
        // events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<string>? TargetDestinationChanged;
        public event Action<List<bool>>? OptionsChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private void NotifyTargetDestinationChanged(string val) => TargetDestinationChanged?.Invoke(val);
        private void NotifyOptionsChanged()
        {
            List<bool> result = new List<bool>();
            for (int i = 0; i < Options.Count; i++)
            {
                result.Add(Options.ElementAt(i).Value.Checked);
            }
            OptionsChanged?.Invoke(result);
        }
        // handlers
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
        // functions
        void ChooseFile()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                string src = openFileDlg.SelectedPath;
                NotifyTargetDestinationChanged(src);
                if (src.Length > 25)
                {
                    src = src.Substring(0, 30);
                }
                ChosenTargetPath.Value = $"{src}...";
            }
        }
    }
}
