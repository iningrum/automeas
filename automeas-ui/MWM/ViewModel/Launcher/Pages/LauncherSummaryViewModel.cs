using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System.Linq;
using System.Windows.Input;

/*
         * Displays summary of user input and estimated time untill measurments
         * will be completed.
         * 
         * Prompts for executing Launch.
         * ----------------------------------------------------------------
         */
namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class Summary
    {
        public Summary(string name)
        {
            Name = name;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
    }
    public class LauncherSummaryViewModel : BaseViewModel
    {
        // internal config
        // ctor
        public LauncherSummaryViewModel(Target T)
        {
            _target = T;
            ChosenOptions = new TrulyObservableCollection<ObservableType<Summary>>();
            foreach (var item in AMDevConfig.SummaryTitles)
            {
                ChosenOptions.Add(new ObservableType<Summary>(new Summary(item)));
            }
            ChosenOptions[0].Value.Description = _target.Name;
            ChosenOptions[1].Value.Description = _target.Destination;
            ChosenOptions[2].Value.Description = GetCheckBoxString();
            ChosenOptions[3].Value.Description = _target.ConfigFileName;
            ChosenOptions[4].Value.Description = _target.NumberOfMoves.ToString();
        }
        // attr
        public TrulyObservableCollection<ObservableType<Summary>> ChosenOptions { get; set; }

        // func
        private string GetCheckBoxString()
        {
            string result = "";
            for (int i = 0; i < _target.Options.Count(); i++)
            {
                if (_target.Options[i] == true)
                {
                    result += $"{AMDevConfig.CheckBoxText_Alternative[i]},  ";
                }

            }
            if (result.Length > 2)
                result = result.Substring(0, result.Length - ",  ".Length);
            return result;
        }
        private ICommand? _SwitchToDashboard;
        public ICommand SwitchToDashboard
        {
            get
            {
                if (_SwitchToDashboard == null)
                {
                    _SwitchToDashboard = new JSRelayCommand(
                        param => this.SwitchWindowToDashboard()
                    );
                }
                return _SwitchToDashboard;
            }
        }
        // functions
        void SwitchWindowToDashboard()
        {
            _target.NotifyChangeWindowToDashboard();
        }
    }
}
