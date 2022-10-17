// [OBSOLETE]   Made obsolete by _Common.Navigator.Launcher
/*
using automeas_ui._Dashboard;
using automeas_ui.MVGenerator;
using System;
using System.Collections.Generic;

namespace automeas_ui._Launcher.Model
{
    internal sealed class Navigator
    {
        // singleton implementation - thread safe
        private static readonly Lazy<Navigator> lazy = new(() => new(typeof(Launcher)));
        public static Navigator Instance { get { return lazy.Value; } }
        // ctor
        private Navigator(Type t)
        {
            _previousWindow = t;
            _currentWindow = t;
        }
        // attr
        public readonly Dictionary<string, Type> RegInit = new()
        {
            {"mvg", typeof(MVG) },              //  MVG - move editor
            {"launcher", typeof(Launcher) },    //  launcher - this model
            {"dashboard", typeof(Dashboard) }   //  dashboard - move manager
        };
        private Type _currentWindow;
        private Type _previousWindow;
        public void ChangeWindow(string id)
        {
            if (id == "\r")
            {
                _currentWindow = _previousWindow;
            }
            else if (id == "\0")
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                Type? getVal;
                if (RegInit.TryGetValue(id, out getVal))
                {
                    _previousWindow = _currentWindow;
                    _currentWindow = getVal;

                }
                else
                {
                    throw new InvalidOperationException("Window id not in register");
                }
            }
            NotifyWindowChanged(_currentWindow);
        }
        // events
        public Action<Type> WindowChanged;
        private void NotifyWindowChanged(Type msg) => WindowChanged?.Invoke(msg);
    }
}
*/