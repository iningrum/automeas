using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    public class RelayCommand : ICommand
    {
        #region Fields 
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion // Fields 
        #region Constructors 
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter) { _execute(parameter); }
        #endregion // ICommand Members 
    }
    public class ViewedPage: INotifyPropertyChanged
    {
        private bool m_IsFocused;

        public bool IsFocused
        {
            get { return m_IsFocused; }
            set { m_IsFocused = value;
                OnPropertyChanged("Property");
            }
        }
        public ViewedPage(bool focused = false)
        {
            IsFocused = focused;
        }
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class PageBarViewModel
    {
        // internal interface
        int NumberOfPages = 8;
        // eof
        public ObservableCollection<ViewedPage> Pages { get; set; }
        public PageBarViewModel()
        {
            Pages = new ObservableCollection<ViewedPage>();
            Pages.Add(new ViewedPage(true));
            for (int i = 1; i < NumberOfPages; i++)
            {
                Pages.Add(new ViewedPage());
            }
        }
        private RelayCommand printCommand;
        public RelayCommand PrintCommand
        {
            get { return printCommand ?? (printCommand = new RelayCommand(param => NextPage())); }
        }
        public void NextPage()
        {
            int pgn = 1;
            foreach (var item in Pages)
            {
                if (item.IsFocused)
                {
                    break;
                }
                pgn++;
            }
            Pages[pgn].IsFocused = true;
        }

    }
}
