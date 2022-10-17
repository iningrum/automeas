using automeas_ui._Common;
using automeas_ui._Launcher.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace automeas_ui._Launcher.ViewModel
{

    public class PageBarViewModel
    {
        // ctor
        public PageBarViewModel(Launcher_MainViewModel master)
        {
            this.master = master;
            //master.PageChanged += LauncherMaster_PageChanged;
            Pages = new();
            Pages.Add(new(true));
            Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            for (int i = 1; i < DevConfig.NumberOfPages; i++)
            {
                Pages.Add(new(false));
                Pages.Last().PropertyChanged += ViewedPage_PropertyChanged;
            }
        }
        // attrs
        private readonly Launcher_MainViewModel master;
        public List<ObservableType<bool>> Pages { get; set; }
        // events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action<int>? PageChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        public void ViewedPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                for (int i = 0; i < Pages.Count(); i++)
                {
                    if (Pages.ElementAt(i).Value == true && master.View.Page != i)
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
            return;
        }
    }
}
