using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;

/*
         * Gets the Name and Description of current setup
         * ----------------------------------------------------------------
         */
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
        // attr
        private const int ID = 1;
        // IBaseViewModel
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
            if (msg != ID)
                return;
            if (this.Name.Value != null)
                _target.Name = this.Name.Value;
            if (this.Description.Value != null)
                _target.Description = this.Description.Value;
        }

        // attr
        public ObservableType<string> Name { get; set; }
        public ObservableType<string> Description { get; set; }
    }
}
