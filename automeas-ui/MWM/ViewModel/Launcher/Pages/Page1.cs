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
            Pages = new TrulyObservableCollection<ObservableType<CheckBox>>();
            Pages.Add(new ObservableType<CheckBox>(new CheckBox(CheckBoxText[0], true, false)));
            Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            for (int i = 1; i < NumberOfPages; i++)
            {
                Pages.Add(new ObservableType<CheckBox>(new CheckBox(CheckBoxText[i])));
                Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            }
        }
        // attrs
        public ObservableType<string> ChosenTargetPath { get; set; }
        private readonly Launcher_MainViewModel master;
        private TrulyObservableCollection<ObservableType<CheckBox>> _Pages;
        public TrulyObservableCollection<ObservableType<CheckBox>> Pages
        {
            get { return _Pages; }
            set
            {
                if (_Pages == value) return;
                _Pages = value;
                NotifyPropertyChanged();
            }
        }
        // events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<int>? PageChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ViewedPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                for (int i = 0; i < Pages.Count(); i++)
                {
                    return;
                    /*if (Pages.ElementAt(i).Value)
                    {
                        PageChanged?.Invoke(i);
                        return;
                    }*/
                }

            }
        }
        public void LauncherMaster_PageChanged(int sender)
        {
            if (sender <= 0)
            {
                sender = Pages.Count - 1;
            }
            else if (sender >= Pages.Count)
            {
                sender = 0;
            }
            //Pages.ElementAt(sender).Value = true;
            PageChanged?.Invoke(sender);
            return;
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
                ChosenTargetPath.Value = openFileDlg.SelectedPath;
            }
        }
    }
}
