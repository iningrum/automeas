using automeas_ui.Core;
using automeas_ui.MWM.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace automeas_ui.MWM.ViewModel
{
    public class PageBarViewModel
    {
        // internal interface
        const int NumberOfPages = 4;
        // ctor
        public PageBarViewModel(Launcher_MainViewModel master)
        {
            this.master = master;
            master.PageChanged += LauncherMaster_PageChanged;
            Pages = new TrulyObservableCollection<ObservableType<bool>>();
            Pages.Add(new ObservableType<bool>(true));
            Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            for (int i = 1; i < NumberOfPages; i++)
            {
                Pages.Add(new ObservableType<bool>(false));
                Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            }
        }
        // attrs
        private readonly Launcher_MainViewModel master;
        private TrulyObservableCollection<ObservableType<bool>> _Pages;
        public TrulyObservableCollection<ObservableType<bool>> Pages
        {
            get { return _Pages; }
            set
            {
                if (_Pages == value) return;
                _Pages = value;
                NotifyPropertyChanged();
            }
        }
        // events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<int>? PageChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ViewedPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                for (int i = 0; i < Pages.Count(); i++)
                {
                    if (Pages.ElementAt(i).Value==true && master.CurrentPage.Value!=i)
                    {
                        PageChanged?.Invoke(i);
                        return;
                    }
                }

            }
        }
        public void LauncherMaster_PageChanged(int sender)
        {
            if (sender < 0)
            {
                sender = Pages.Count - 1;
            }
            else if (sender >= Pages.Count)
            {
                sender = 0;
            }
            Pages.ElementAt(sender).Value = true;
            //PageChanged?.Invoke(sender);
            return;
        }
        // NO handlers
    }
}
