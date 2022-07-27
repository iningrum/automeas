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
        public Target()
        {
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
        public string Destination;
        public string Name;
        public string Description;
        public List<bool> Options;
    }
}
