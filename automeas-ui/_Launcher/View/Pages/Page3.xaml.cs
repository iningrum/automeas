﻿using automeas_ui._Launcher.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace automeas_ui._Launcher.View.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Page3.xaml
    /// </summary>
    public partial class Page3 : UserControl
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Button_HandleDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string filename = System.IO.Path.GetFileName(files[0]);
                string full_path = files[0];
                var vm = (UploadConfigFileViewModel)this.DataContext;
                vm.DragDropFile(filename, full_path);
            }
        }

        private void IntegerUpDown_InputValidationError(object sender, Xceed.Wpf.Toolkit.Core.Input.InputValidationErrorEventArgs e)
        {
            var vm = (UploadConfigFileViewModel)this.DataContext;
            vm.NOfRepeatsInt.Value = 1;
        }
    }
}