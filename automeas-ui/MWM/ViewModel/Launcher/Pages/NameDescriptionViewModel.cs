using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class NameDescriptionViewModel : BaseViewModel, IBaseViewModel
    {
        // ctor
        public NameDescriptionViewModel()
        {
            Name = new ObservableType<string>("");
            Description = new ObservableType<string>("");
        }

        public void Bind(Target T, Action<int> handler)
        {
            _target = T;
            _target.PageChangedEvent += HandlePageChanged;
        }

        public void Load(Target T)
        {
            Bind(T, HandlePageChanged);
            this.Name.Value = _target.Name;
            this.Description.Value = _target.Description;
        }

        public void HandlePageChanged(int msg)
        {
            throw new NotImplementedException();
        }

        // handlers
        // attr
        public ObservableType<string> Name { get; set; }
        public ObservableType<string> Description { get; set; }
    }
}
