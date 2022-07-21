﻿using automeas_ui.Core;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace automeas_ui.MWM.ViewModel
{
    
    public class ViewedPage: INotifyPropertyChanged
    {
        private bool m_IsFocused;

        public bool IsFocused
        {
            get { return m_IsFocused; }
            set { m_IsFocused = value;
                OnPropertyChanged("IsFocused");
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
        const int NumberOfPages = 5;
        // eof
        private TrulyObservableCollection<ViewedPage> _Pages;
        public TrulyObservableCollection<ViewedPage> Pages
        {
            get { return _Pages; }
            set
            {
                if (_Pages == value) return;
                _Pages = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PageBarViewModel()
        {
            Pages = new TrulyObservableCollection<ViewedPage>();
            Pages.Add(new ViewedPage(true));
            for (int i = 1; i < NumberOfPages; i++)
            {
                Pages.Add(new ViewedPage());
            }
        }
        private ICommand? _npCommand;
        private ICommand? _ppCommand;
        public ICommand NpCommad
        {
            get
            {
                if (_npCommand == null)
                {
                    _npCommand = new JSRelayCommand(
                        param => this.NextPage(),
                        param => this.CanSave()
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
                        param => this.PreviousPage(),
                        param => this.CanSave()
                    );
                }
                return _ppCommand;
            }
        }
        private bool CanSave()
        {
            // Verify command can be executed here
            return true;
        }

        private void NextPage()
        {
            int pgn = 1;
            if (Pages.Last().IsFocused) // last page - goto first page
            {
                Pages.First().IsFocused = true;
                return;
            }
            for (int i = 0; i < Pages.Count()-1; i++)
            {
                if (Pages[i].IsFocused)
                {
                    break;
                }
                pgn++;
            }
            Pages[pgn].IsFocused = true;
            CollectionViewSource.GetDefaultView(Pages).Refresh();
        }
        private void PreviousPage()
        {
            int pgn = -1;
            if (Pages.First().IsFocused) // first page - goto last page
            {
                Pages.Last().IsFocused = true;
                return;
            }
            for (int i = 0; i < Pages.Count() - 1; i++)
            {
                if (Pages[i].IsFocused)
                {
                    break;
                }
                pgn++;
            }
            Pages[pgn].IsFocused = true;
            CollectionViewSource.GetDefaultView(Pages).Refresh();
        }

    }
}
