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
        private static readonly Lazy<Target> lazy = new(() => new());
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
       
        public string Destination;
        public string Name;
        public string Description;
        public string ConfigFilePath;
        public string ConfigFileName;
        public int NumberOfMoves;
        public List<bool> Options;
    }
}
