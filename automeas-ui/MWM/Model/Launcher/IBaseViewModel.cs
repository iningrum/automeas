using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.Launcher
{
    internal interface IBaseViewModel
    {
        public void Bind(Target T, Action<int> handler);
        public void Load(Target T); // call after ctor, pass args to bind
        public void HandlePageChanged(int msg);
    }
}
