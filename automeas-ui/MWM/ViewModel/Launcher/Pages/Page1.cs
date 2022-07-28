using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

/*
         * Prompts user to choose where the results should be uploaded
         * and in which form.
         * ----------------------------------------------------------------
         */
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
    public class Page1 : BaseViewModel, IBaseViewModel
    {
        // internal interface
        const int ID = 0;
        // ctor
        public Page1()
        {
            ChosenTargetPath = new ObservableType<string>("");
            Options = new TrulyObservableCollection<ObservableType<CheckBox>>();
            Options.Add(new ObservableType<CheckBox>(new CheckBox(AMDevConfig.CheckBoxText[0], true, false)));
            for (int i = 1; i < AMDevConfig.CheckBoxText.Count(); i++)
            {
                Options.Add(new ObservableType<CheckBox>(new CheckBox(AMDevConfig.CheckBoxText[i])));
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
                if (src == null || src.Length == 0)
                {
                    return;
                }
                NotifyTargetDestinationChanged(src);
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
                _target.Destination = this.ChosenTargetPath.Value;
                List<bool> options = new List<bool>();
                foreach (var item in Options)
                {
                    options.Add(item.Value.Checked);
                }
                _target.Options = options;
            }

        }

        public void Bind(Target T, Action<int> handler)
        {
            _target = T;
            _target.PageChangedEvent += HandlePageChanged;
            if (_target.Options == null || _target.Options.Count < AMDevConfig.CheckBoxText.Count())
            {
                _target.Options = new List<bool>(AMDevConfig.CheckBoxText.Count());
                _target.Options.Add(true);
                for (int i = 1; i < AMDevConfig.CheckBoxText.Count(); i++)
                {
                    _target.Options.Add(false);
                }
            }
        }

        public void Load(Target T)
        {
            Bind(T, HandlePageChanged);
            this.ChosenTargetPath.Value = _target.Destination;
            for (int i = 0; i < Options.Count; i++)
            {
                Options[i].Value.Checked = _target.Options[i];
            }
        }
    }
}
