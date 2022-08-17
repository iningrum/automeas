using automeas_ui._Launcher.Model;
using automeas_ui._Launcher.ViewModel.Pages;
using automeas_ui.MWM.Model.Launcher;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target = automeas_ui._Launcher.Model.Target;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;
using automeas_ui._Common;
using Navigator = automeas_ui._Common.Navigator;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using automeas_ui.MWM.ViewModel.Launcher.Pages;
using Page1 = automeas_ui._Launcher.ViewModel.Pages.Page1;

namespace automeas_ui._Launcher.ViewModel
{
    /// <summary>
    /// Determines current view, page number and page title.
    /// Is observable and databound to UI via <c>Launcher_MainViewModel.View</c>
    /// </summary>
    [ObservableObject]
    public partial class CurrentView
    {
        [ObservableProperty]
        private ILauncherPage current;
        [ObservableProperty]
        private int page;
        [ObservableProperty]
        private string title;
    }
    /// <summary>
    /// Main viewm for launcher.
    /// Used for navigation between subviews (pages).
    /// </summary>
    public partial class Launcher_MainViewModel
    {
        /// <summary>
        /// Contains observable objects that are databound.
        /// </summary>
        public CurrentView View { get; set; }
        /// <summary>
        /// Only one item in List should be true.
        /// Used for UI element that indicates the current page and 
        /// allows for switching between pages.
        /// </summary>
        public List<ObservableType<bool>> PageBar { get; set; }
        /// <summary>
        /// Init view, populate page bar
        /// </summary>
        public Launcher_MainViewModel()
        {
            View = new();
            View.Current = Navigator.Launcher.Change<ILauncherPage>("0");
            { // load page bar
                PageBar = new();
                PageBar.Add(new ObservableType<bool>(true));
                PageBar.Last().PropertyChanged += HandlePageBarChanged;
                for (int i = 1; i < DevConfig.NumberOfPages; i++)
                {
                    PageBar.Add(new ObservableType<bool>(false));
                    PageBar.Last().PropertyChanged += HandlePageBarChanged;
                }
            }
            Target.Instance.Launcher_MainViewModel_SetMaster(this);
            View.Page = 0;
            View.Title = DevConfig.PageTitles[View.Page];
        }
        [RelayCommand]
        void NextPage() => RenderNewPage(View.Page + 1);
        [RelayCommand]
        void PreviousPage() => RenderNewPage(View.Page-1);
        void HandlePageBarChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value" && _ignorePageBar==false)
            {
                for (int i = 0; i < PageBar.Count(); i++)
                {
                    if (PageBar.ElementAt(i).Value == true)
                    {
                        RenderNewPage(i);
                        return;
                    }
                }

            }
        }
    }
    public partial class Launcher_MainViewModel
    {// auxiliary functions
        private void RenderNewPage(int i)
        {
            { // range check i
                i = (i < 0) ? PageBar.Count() - 1 : (i >= PageBar.Count()) ? 0 : i;
            }
            var oldViewPage = View.Page;
            _ignorePageBar = true;
            {
                View.Page = i;
                PageBar[oldViewPage].Value = false;
                PageBar[View.Page].Value = true;
                View.Current.Save();
                View.Current = Navigator.Launcher.Change<ILauncherPage>(View.Page.ToString());
                View.Title = DevConfig.PageTitles[View.Page];
            }
            _ignorePageBar = false;
        }
        private bool _ignorePageBar = false;
    }
    
}
