using automeas_ui._Dashboard;
using automeas_ui._Launcher.Model;
using automeas_ui.MVGenerator;
using System;
using System.Collections.Generic;
using System.Windows;
using llc = automeas_ui._Launcher.Launcher;
namespace automeas_ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            { // Set & open default Window
                automeas_ui._Common.Navigator.App._handle = this;
                mw = automeas_ui._Common.Navigator.App.Change<Window>("\r");
                mw.Show();
            }
            
        }
        private Window? mw;
        public void sSwitch(string id)
        {
            Window toBeClosed = mw;
            {
                mw = new();
                mw = automeas_ui._Common.Navigator.App.Change<Window>(id);
                mw.Show();
            }
            toBeClosed.Close();
        }
    }
}
