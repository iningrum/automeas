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
            { // bind to target
                Target.Instance.PageChangedEvent += HandlePageChanged;
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

        // attr
        public ObservableType<string> Name { get; set; }
        public ObservableType<string> Description { get; set; }
    }
}
