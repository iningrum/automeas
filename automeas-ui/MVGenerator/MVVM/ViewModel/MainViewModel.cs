﻿using automeas_ui.Core;
using automeas_ui.MVGenerator.MVVM.Model;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal partial class MainViewModel
    {
        // ctor
        public MainViewModel()
        {
            MVGTarget.Instance.ViewNavigate += HandleViewNavigate;
            CurrentPage = new ObservableType<int>(0);
            CurrentView = new ObservableType<object>(null);
            mmView = new ObservableType<MinimapViewModel>(new MinimapViewModel());
        }
        // attr
        public ObservableType<int> CurrentPage { get; set; }
        public ObservableType<object> CurrentView { get; set; }
        public ObservableType<MinimapViewModel> mmView { get; set; }
        // cmd
        [RelayCommand]
        public void SaveMVGData()
        {
            // open file browser, save data, generate data
            Navigator.Instance.ChangeWindow("\r");
        }
        /*[RelayCommand]
        public void OpenPullCreator()
        {
            CurrentPage.Value = 2;
            CurrentView.Value = new MoveCreatorViewModel(false);
        }
        [RelayCommand]
        public void OpenPushCreator()
        {
            CurrentPage.Value = 1;
            CurrentView.Value = new MoveCreatorViewModel(true);
        }
        [RelayCommand]
        public void ReturnToMainView()
        {
            CurrentPage.Value = 0;
            CurrentView.Value = null;
        }*/
        public void HandleViewNavigate(string id)
        {
            switch (id)
            {
                case "main":
                    CurrentPage.Value = 0;
                    CurrentView.Value = null;
                    break;
                case "push":
                    CurrentPage.Value = 1;
                    mmView.Value = new MinimapViewModel();
                    CurrentView.Value = new MoveCreatorViewModel(true);
                    break;
                case "pull":
                    CurrentPage.Value = 1;
                    mmView.Value = new MinimapViewModel();
                    CurrentView.Value = new MoveCreatorViewModel(false);
                    break;
            }   
        }
    }
    
}
