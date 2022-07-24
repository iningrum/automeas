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
        public Target(Page1 master)
        {
            _Master = master;
            _Master.OptionsChanged += Master_OptionsChanged;
            _Master.TargetDestinationChanged += Master_DestinationChanged;
        }
        // events
        // handlers
        void Master_OptionsChanged(List<bool> msg) => Options = msg;
        void Master_DestinationChanged(string msg) => Destination = msg;
        // attr
        private readonly Page1 _Master;
        public string? Destination { get; set; }
        public List<bool>? Options { get; set; }
    }
}
