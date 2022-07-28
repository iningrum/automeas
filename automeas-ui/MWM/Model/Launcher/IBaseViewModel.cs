using System;

/*
         * Interface used by subviews in Launcher 
         * ----------------------------------------------------------------
         */
namespace automeas_ui.MWM.Model.Launcher
{
    internal interface IBaseViewModel
    {
        public void HandlePageChanged(int msg); // if ID==msg then upload all the data collected into this._target
    }
}
