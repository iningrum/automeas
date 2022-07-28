using System;

/*
         * Interface used by subviews in Launcher 
         * ----------------------------------------------------------------
         */
namespace automeas_ui.MWM.Model.Launcher
{
    internal interface IBaseViewModel
    {
        //public void Bind(Target T, Action<int> handler); // subscribes to Target events
        //public void Load(Target T); // call after ctor, pass args to bind
        //          ^ retreives user input from Target, that's why it should be called ONCE, just after initialization of new instance.
        public void HandlePageChanged(int msg); // if ID==msg then upload all the data collected into this._target
    }
}
