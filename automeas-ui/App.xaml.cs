using automeas_ui.MVGenerator;
using automeas_ui.MWM.Model.Launcher;
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
            MVG mvg = new MVG();
            var x = (Launcher_MainViewModel)launcher.DataContext;
            Target.Instance.ChangeWindowToDashboard += HandleWindowToDashboard;
            Target.Instance.ChangeWindowToMVG += HandleWindowToMVG;
            //mw = launcher;
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
        private void InitMVG()
        {
            MVG MoVeGenerator = new MVG();
            mw = MoVeGenerator;
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
        public void HandleWindowToMVG()
        {
            Switch(InitMVG);
        }
    }
}
