using automeas_ui.MWM.ViewModel;
using System;
using System.Collections.Generic;
/*
         * Launcher_MainViewModel is the main view that manages navigation
         * Target (Launcher_MainViewModel._target) serves as intermediary
         * between MainViewModel and pages 1-N
         * ----------------------------------------------------------------
         * ViewModel deinit => upload user input to Target
         * ViewModel init => restore already input data from Target
         */
namespace automeas_ui.MWM.Model.Launcher
{
    public sealed class Target
    {
        // singleton implementation - thread safe
        private static readonly Lazy<Target> lazy = new Lazy<Target>(() => new Target());
        public static Target Instance { get { return lazy.Value; } }
        // ctor
        private Target()
        {
            ConfigFilePath = "";
            ConfigFileName = "";
            Destination = "File Path     📁  ";
            Name = "Sample name";
            Description = "Sample description";
            NumberOfMoves = 1;
        }
        // events

        public event Action<int>? PageChangedEvent;
        /*
         * Launcher_MainViewModel.PageNoLonger Relevant  ->
         * -> this.PageChangedEvent -> all pages
         * ------------------------------------------------
         * It prompts old view to upload all user data that it's got
         * into Target
         */
        public event Action<List<string>>? ChangeWindowToDashboard;
        public event Action ChangeWindowToMVG;
        /*
         *  LauncherSummaryViewModel.SwitchToDashboard() ->
         *  -> this.ChangeWindowToDashboard -> App.HandleWindowToDashboard()
         *  -----------------------------------------------
         *  Used solely to close Launcher and open Dashboard
         */
        // senders
        private void NotifyPageChanged(int msg) => PageChangedEvent?.Invoke(msg);
        // handlers
        // attr
        private Launcher_MainViewModel _master;
        public void Launcher_MainViewModel_SetMaster(Launcher_MainViewModel master)
        {
            _master = master;
            master.PageNoLongerRelevant += NotifyPageChanged;
            return;
        }
        public string Destination;
        public string Name;
        public string Description;
        public string ConfigFilePath;
        public string ConfigFileName;
        public int NumberOfMoves;
        public List<bool> Options;
        public void NotifyChangeWindowToDashboard() => ChangeWindowToDashboard?.Invoke(new List<string>());
        public void NotifyChangeWindowToMVG() => ChangeWindowToMVG?.Invoke();
    }
}
