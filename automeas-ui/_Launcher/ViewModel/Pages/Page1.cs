using automeas_ui._Common;
using automeas_ui._Launcher.Model;
using automeas_ui.Core;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Target = automeas_ui._Launcher.Model.Target;

namespace automeas_ui._Launcher.ViewModel.Pages
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
    public partial class Page1 : ILauncherPage
    {
        // internal interface
        const int ID = 0;
        // ctor
        public Page1()
        {
            { // load from Target
                if (Target.Instance.Options == null || Target.Instance.Options.Count < DevConfig.CheckBoxText.Count())
                {
                    Target.Instance.Options = new List<bool>(DevConfig.CheckBoxText.Count());
                    Target.Instance.Options.Add(true);
                    for (int i = 1; i < DevConfig.CheckBoxText.Count(); i++)
                    {
                        Target.Instance.Options.Add(false);
                    }
                }
                this.ChosenTargetPath = new ObservableType<string>(Target.Instance.Destination);
                this.Options = new TrulyObservableCollection<ObservableType<CheckBox>>();
                Options.Add(new ObservableType<CheckBox>(new CheckBox(AMDevConfig.CheckBoxText[0], true, false)));
                for (int i = 1; i < DevConfig.CheckBoxText.Count(); i++)
                {
                    Options.Add(new ObservableType<CheckBox>(new CheckBox(AMDevConfig.CheckBoxText[i], Target.Instance.Options[i])));
                }

            }

        }
        // attrs
        public ObservableType<string> ChosenTargetPath { get; set; }
        private TrulyObservableCollection<ObservableType<CheckBox>> _Options;
        public TrulyObservableCollection<ObservableType<CheckBox>> Options
        {
            get { return _Options; }
            set
            {
                if (_Options == value) return;
                _Options = value;
            }
        }
        // functions
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
                //NotifyTargetDestinationChanged(src);
                if (src.Length > 25)
                {
                    src = src.Substring(0, 28);
                }
                ChosenTargetPath.Value = $"{src}... ";
            }
        }

        public void HandlePageChanged(int msg)
        {
            if (ID == msg)
            {
                Target.Instance.Destination = this.ChosenTargetPath.Value;
                List<bool> options = new List<bool>();
                foreach (var item in Options)
                {
                    options.Add(item.Value.Checked);
                }
                Target.Instance.Options = options;
            }

        }

        public void Save()
        {
            HandlePageChanged(ID);
        }
    }
}
