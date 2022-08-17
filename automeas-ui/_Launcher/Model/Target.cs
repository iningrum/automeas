using automeas_ui._Launcher.ViewModel;
using System;
using System.Collections.Generic;

namespace automeas_ui._Launcher.Model
{
    /// <summary>
    /// Singleton <c>Target</c> serves as communication layer between different view models belonging to <c>Launcher</c>
    /// page view models load and store data in this class.
    /// </summary>
    public sealed class Target
    {
        // singleton implementation - thread safe
        private static readonly Lazy<Target> lazy = new Lazy<Target>(() => new Target());
        public static Target Instance { get { return lazy.Value; } }
        // ctor
        /// <summary>
        /// Default values.
        /// </summary>
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
        /// <summary>
        /// [Obsolete] Prompts old view to uppload all collected input
        /// </summary>
        public event Action<int>? PageChangedEvent;
        /*
         * Launcher_MainViewModel.PageNoLonger Relevant  ->
         * -> this.PageChangedEvent -> all pages
         * ------------------------------------------------
         * It prompts old view to upload all user data that it's got
         * into Target
         */
        /// <summary>
        /// [Obsolete] Tells App that window should be changed from Launcher to Dashboard
        /// </summary>
        public event Action<List<string>>? ChangeWindowToDashboard;
        /// <summary>
        /// [Obsolete] Tells App that window should be changed to MVG
        /// </summary>
        public event Action ChangeWindowToMVG;
        /// <summary>
        /// [Obsolete] Tells App that window should be changed to Launcher
        /// </summary>
        public event Action ChangeMVGToLauncher;
        /*
         *  LauncherSummaryViewModel.SwitchToDashboard() ->
         *  -> this.ChangeWindowToDashboard -> App.HandleWindowToDashboard()
         *  -----------------------------------------------
         *  Used solely to close Launcher and open Dashboard
         */
        // senders
        // handlers
        // attr
        private Launcher_MainViewModel _master;
        public void Launcher_MainViewModel_SetMaster(Launcher_MainViewModel master)
        {
            _master = master;
            return;
        }
        public string Destination;
        public string Name;
        public string Description;
        public string ConfigFilePath;
        public string ConfigFileName;
        public int NumberOfMoves;
        public List<bool> Options;
    }
}
