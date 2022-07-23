using automeas_ui.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    internal static class PgeTitles
    {
        static public string Get(int id)
        {
            List<string> results = new List<String>{
                "Katalog docelowy",
                "Opracowanie pomiarów",
                "Nazwa i opis próby pomiarowej",
                "Konfiguracja próby",
                "Podsumowanie"
            };
            return results.ElementAt(id);
        }

    }
    public class TtleStr : INotifyPropertyChanged
    {
        private string m_Title;
        public string Title
        {
            get { return m_Title; }
            set
            {
                m_Title = value;
                OnPropertyChanged("Title");
            }
        }
        public TtleStr(int value = 0)
        {
            Title = PageTitles.Get(value);
        }
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class ObservableInt : INotifyPropertyChanged
    {
        private int m_Val;
        public int Val
        {
            get { return m_Val; }
            set
            {
                m_Val = value;
                OnPropertyChanged("Val");
            }
        }
        public ObservableInt(int value = 0)
        {
            Val = value;
        }
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class Launcher_MainViewModel
    {
        private object _currentView2;
        private PageBarViewModel _PageBarView;
        public int CurrentPage { get; set; }
        public ObservableInt ObservableCurrentPage { get; set; }
        public TtleStr CurrentPageTitle { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public Launcher_MainViewModel()
        {
            _PageBarView = new PageBarViewModel(this);
            CurrentPageTitle = new TtleStr();
            _PageBarView.PageChanged += _PageBarView_PageChanged;
            PageBarView = _PageBarView;
            CurrentPage = 0;
            ObservableCurrentPage = new ObservableInt();
        }

        private void _PageBarView_PageChanged(int sender)
        {
            CurrentPage = sender;
            ObservableCurrentPage.Val = CurrentPage;
            CurrentPageTitle.Title = PgeTitles.Get(sender);
            return;
        }

        public PageBarViewModel PageBarView
        {
            get { return _PageBarView; }
            set
            {
                _PageBarView = value;
            }
        }

        public object CurrentView2
        {
            get { return _currentView2; }
            set
            {
                _currentView2 = value;
            }
        }
        public event Action<int> PageChanged;
        void NextPage() => PageChanged?.Invoke(CurrentPage+1);
        void PreviousPage() => PageChanged?.Invoke(CurrentPage-1);
        private ICommand? _npCommand;
        private ICommand? _ppCommand;
        public ICommand NpCommad
        {
            get
            {
                if (_npCommand == null)
                {
                    _npCommand = new JSRelayCommand(
                        param => this.NextPage()
                    );
                }
                return _npCommand;
            }
        }
        public ICommand PpCommad
        {
            get
            {
                if (_ppCommand == null)
                {
                    _ppCommand = new JSRelayCommand(
                        param => this.PreviousPage()
                    );
                }
                return _ppCommand;
            }
        }
 
    }
}
