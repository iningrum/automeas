using automeas_ui.MWM.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace automeas_ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitLauncher();
        }
        private Window? mw;
        private void InitLauncher()
        {
            Launcher launcher = new Launcher();
            var x = (Launcher_MainViewModel)launcher.DataContext;
            x.Config.ChangeWindowToDashboard += HandleWindowToDashboard;
            mw = launcher;
            mw.Show();
            return;
        }
        private void InitDashboard()
        {
            Dashboard dashboard = new Dashboard();
            mw = dashboard;
            mw.Show();
            return;
        }
        private void Switch(Action initializer)
        {
            Window toBeClosed = mw;
            initializer();
            toBeClosed.Close();
        }
        public void HandleWindowToDashboard(List<string> msg)
        {
            Switch(InitDashboard);
        }
    }
}
