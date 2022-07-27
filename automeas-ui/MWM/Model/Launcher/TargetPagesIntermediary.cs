using automeas_ui.MWM.ViewModel.Launcher.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.Launcher
{
    public class TargetPagesIntermediary
    {
        // ctor
        public TargetPagesIntermediary(Target T, Page1 P1, NameDescriptionViewModel P2)
        {
            
        }
        public void Bind(Target T, object H)
        {
        }
        // attr
        private readonly Target _target;
        private readonly Page1 _page1;
        private readonly NameDescriptionViewModel _page2;
        // events
        public event Action<int>? PageChangedEvent;
        // senders
        private void NotifyPageChanged(int msg) => PageChangedEvent?.Invoke(msg);
        // handlers
        public void HandleGet_Page1_Data(string path, List<bool> options)
        {
            _target.Destination = path;
            _target.Options = options;
        }
        public void HandleGet_Page2_Data(string name, string description)
        {
            _target.Name = name;
            _target.Description = description;
        }
    }
}
