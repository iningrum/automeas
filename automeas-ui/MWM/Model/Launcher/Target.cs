using automeas_ui.MWM.ViewModel;
using automeas_ui.MWM.ViewModel.Launcher.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.Launcher
{
    public class Target
    {
        // ctor
        public Target(Launcher_MainViewModel master)
        {
            _master = master;
            master.PageNoLongerRelevant += NotifyPageChanged;
            Destination = "File Path     📁  ";
            Name = "Sample name";
            Description = "Sample description";
        }
        // events
        public event Action<int>? PageChangedEvent;
        // senders
        private void NotifyPageChanged(int msg) => PageChangedEvent?.Invoke(msg);
        // handlers
        // attr
        private readonly Launcher_MainViewModel _master;
        public string Destination;
        public string Name;
        public string Description;
        public string ConfigFilePath;
        public string ConfigFileName;
        public List<bool> Options;
    }
}
