using automeas_ui._Common;
using automeas_ui._Launcher.Model;
using automeas_ui.Core;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using Target = automeas_ui._Launcher.Model.Target;

namespace automeas_ui._Launcher.ViewModel.Pages
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
    public partial class LauncherSummaryViewModel : ILauncherPage
    {
        // internal config
        // ctor
        public LauncherSummaryViewModel()
        {
            ChosenOptions = new TrulyObservableCollection<ObservableType<Summary>>();
            foreach (var item in DevConfig.SummaryTitles)
            {
                ChosenOptions.Add(new ObservableType<Summary>(new Summary(item)));
            }
            ChosenOptions[0].Value.Description = Target.Instance.Name;
            ChosenOptions[1].Value.Description = Target.Instance.Destination;
            ChosenOptions[2].Value.Description = GetCheckBoxString();
            ChosenOptions[3].Value.Description = Target.Instance.ConfigFileName;
            ChosenOptions[4].Value.Description = Target.Instance.NumberOfMoves.ToString();
        }
        // attr
        public TrulyObservableCollection<ObservableType<Summary>> ChosenOptions { get; set; }

        public void Save()
        {
            return;
        }

        // func
        private string GetCheckBoxString()
        {
            string result = "";
            for (int i = 0; i < Target.Instance.Options.Count(); i++)
            {
                if (Target.Instance.Options[i] == true)
                {
                    result += $"{DevConfig.CheckBoxText_Alternative[i]},  ";
                }

            }
            if (result.Length > 2)
                result = result.Substring(0, result.Length - ",  ".Length);
            return result;
        }
        // functions
        [RelayCommand]
        void SwitchToDashboard()
        {
            ((App)automeas_ui._Common.Navigator.App._handle).sSwitch("Dashboard");
        }
    }
}
