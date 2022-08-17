using automeas_ui._Launcher.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target = automeas_ui._Launcher.Model.Target;

namespace automeas_ui._Launcher.ViewModel.Pages
{
    public class NameDescriptionViewModel : ILauncherPage
    {
        // ctor
        public NameDescriptionViewModel()
        {
            { // bind to target
                this.Name = new ObservableType<string>(Target.Instance.Name);
                this.Description = new ObservableType<string>(Target.Instance.Description);
            }
        }
        // attr
        private const int ID = 1;
        // IBaseViewModel
        public void HandlePageChanged(int msg)
        {
            if (msg != ID)
                return;
            if (this.Name.Value != null)
                Target.Instance.Name = this.Name.Value;
            if (this.Description.Value != null)
                Target.Instance.Description = this.Description.Value;
        }

        public void Save()
        {
            HandlePageChanged(ID);
        }

        // attr
        public ObservableType<string> Name { get; set; }
        public ObservableType<string> Description { get; set; }
    }
}
