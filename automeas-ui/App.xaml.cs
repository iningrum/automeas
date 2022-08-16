﻿using automeas_ui.MVGenerator;
using automeas_ui._Launcher.Model;
using automeas_ui.MWM.ViewModel;
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
            InitLauncher();
        }
        private Window? mw;
        private void InitLauncher()
        {
            llc launcher = new();
            //var x = (Launcher_MainViewModel)launcher.DataContext;
            Navigator.Instance.WindowChanged += HandleWindowChanged;
            mw = launcher;
            mw.Show();
            return;
        }
        private void InitAnyWindow(Type T)
        {
            mw = (Window?)Activator.CreateInstance(T);
            if (mw == null)
            {
                throw new Exception("It should not even be possible for app.mw to be null");
            }
            mw.Show();
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
        private void ReInitLauncher()
        {
            mw = new llc();
            mw.Show();
            return;
        }
        private void Switch(Action initializer)
        {
            Window toBeClosed = mw;
            initializer();
            toBeClosed.Close();
        }
        private void SwitchAnyWindow(Type window)
        {
            Window toBeClosed = mw;
            InitAnyWindow(window);
            toBeClosed.Close();
        }
        public void HandleWindowToDashboard(List<string> msg)
        {
            Switch(InitDashboard);
        }
        public void HandleUnsavedMVG()
        {
            Switch(ReInitLauncher);
        }
        public void HandleWindowToMVG()
        {
            Switch(InitMVG);
        }
        public void HandleWindowChanged(Type msg) => SwitchAnyWindow(msg);
    }
}
