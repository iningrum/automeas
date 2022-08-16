using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._Common
{
    /// <summary>
    /// Used for navigation - changing views, windows or pages.
    /// <c>Navigator</c> consists of register which converts string into Type.
    /// This type can be then used for reinstantiation of
    /// register can be only loaded once
    /// </summary>
    /// <typeparam name="T"> Default type stored</typeparam>
    public sealed class Navigator<T>
    {
        // singleton implementation - thread safe
        private static readonly Lazy<Navigator<T>> lazy = new Lazy<Navigator<T>>(() => new Navigator<T>(typeof(T)));
        public static Navigator<T> Instance { get { return lazy.Value; } }
        // ctor
        /// <summary>
        /// Default values
        /// </summary>
        /// <param name="t"> Default value of _previous and _current</param>
        public Navigator(Type t)
        {
            _previous = t;
            _current = t;
            _reg = new();
        }
        private Dictionary<string, Type> _reg;
        private bool _locked = false;
        private Type _previous;
        private Type _current;
        // functions
        /// <summary>
        /// Find id in register, return new instance of matched Type.
        /// If notify==true notify subscribers about the change.
        /// </summary>
        /// <typeparam name="Z"> Base type common to all Types in register.
        /// To be treated like type cast.</typeparam>
        /// <param name="id"> Id of item that will be instantiated</param>
        /// <param name="notify"> (optional) notify subscribers about the change.</param>
        /// <returns> Object of base type Z that is registered</returns>
        /// <exception cref="InvalidOperationException">Thrown when given id does not exist in register</exception>
        /// <exception cref="Exception"> Thrown when object instantiation failed.</exception>
        public Z Change<Z>(string id, bool notify=true)
        {
            if (id == "\r")
            {
                _current = _previous;
            }
            else if (id == "\0")
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                Type? getVal;
                if (_reg.TryGetValue(id, out getVal))
                {
                    _previous = _current;
                    _current = getVal;

                }
                else
                {
                    throw new InvalidOperationException("Window id not in register");
                }
            }
            if (notify)
            {
                NotifyWindowChanged(_current);
            }
            Z? result = (Z?)Activator.CreateInstance(_current);
            if (result != null) { return (Z)result; }
            else { throw new Exception("Instantiation of type failed"); }
        }
        /// <summary>
        /// Find id in register, match to corresponding type,
        /// return this type via notification to all suscribers.
        /// </summary>
        /// <param name="id"> Id of item that will be instantiated</param>
        /// <exception cref="InvalidOperationException">Thrown when given id does not exist in register</exception>
        public void Change(string id)
        {
            if (id == "\r")
            {
                _current = _previous;
            }
            else if (id == "\0")
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                Type? getVal;
                if (_reg.TryGetValue(id, out getVal))
                {
                    _previous = _current;
                    _current = getVal;

                }
                else
                {
                    throw new InvalidOperationException("Window id not in register");
                }
            }
            NotifyWindowChanged(_current);
        }
        public void LoadRegister(Dictionary<string, Type> register)
        {
            if(_locked == true) { return; }
            _locked = true;
            _reg = register;
        }
        public Action<Type>? WindowChanged;

        private void NotifyWindowChanged(Type msg) => WindowChanged?.Invoke(msg);
    }
}
