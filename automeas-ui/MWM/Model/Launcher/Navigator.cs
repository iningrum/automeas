using automeas_ui.MVGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace automeas_ui.MWM.Model.Launcher
{
    public sealed class Navigator
    {
        // singleton implementation - thread safe
        private static readonly Lazy<Navigator> lazy = new Lazy<Navigator>(() => new Navigator(typeof(automeas_ui.Launcher)));
        public static Navigator Instance { get { return lazy.Value; } }
        // ctor
        private Navigator(Type t)
        {
            _previousWindow = t;
            _currentWindow = t;
        }
        // attr
        public readonly Dictionary<string, Type> RegInit = new Dictionary<string, Type>()
        {
            {"mvg", typeof(MVG) },
            {"launcher", typeof(automeas_ui.Launcher) },
            {"dashboard", typeof(Dashboard) }
        };
        private Type _currentWindow;
        private Type _previousWindow;
        public void ChangeWindow(string id)
        {
            if (id== "previous")
            {
                _currentWindow = _previousWindow;
            }
            if (id == "shutdown")
            {
                throw new NotImplementedException();
            }
            else
            {
                Type? getVal;
                if(RegInit.TryGetValue(id, out getVal))
                {
                    _previousWindow = _currentWindow;
                    _currentWindow = getVal;
                    NotifyWindowChanged(_currentWindow);
                    return;
                    
                }
                throw new InvalidOperationException("Window id not in register");
            }
        }
        // events
        public Action<Type> WindowChanged;
        private void NotifyWindowChanged(Type msg) => WindowChanged?.Invoke(msg);
    }
}
