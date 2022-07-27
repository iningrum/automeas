using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<string> _summaryTitles = new List<string>
        {
            "Nazwa próby:\t\t",
            "Katalog docelowy:\t",
            "Wybrane formaty:\t",
            "Typy ruchów:\t\t",
            "Ilość krokow:\t\t",
            "Szacowany Czas:\t\t"
        };
        // ctor
        public LauncherSummaryViewModel(Target T)
        {
            _target = T;
            ChosenOptions = new TrulyObservableCollection<ObservableType<Summary>>();
            foreach (var item in _summaryTitles)
            {
                ChosenOptions.Add(new ObservableType<Summary>(new Summary(item)));
            }
            ChosenOptions[0].Value.Description = _target.Name;
            ChosenOptions[1].Value.Description = _target.Destination;
        }
        // attr
        public TrulyObservableCollection<ObservableType<Summary>> ChosenOptions { get; set; }
    }
}
