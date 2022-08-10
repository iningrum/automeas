﻿using automeas_ui.Core;
using automeas_ui.MWM.Model;
using automeas_ui.MWM.Model.Launcher;
using System;
using System.Windows.Input;

/*
         * Handles, loads and verifies integrity of Move file
         * ----------------------------------------------------------------
         */
namespace automeas_ui.MWM.ViewModel.Launcher.Pages
{
    public class UploadConfigFileViewModel : BaseViewModel, IBaseViewModel
    {
        // ctor
        public UploadConfigFileViewModel()
        {
            { // load Target
                Target.Instance.PageChangedEvent += HandlePageChanged;
                NOfRepeatsInt = new ObservableType<int>(Target.Instance.NumberOfMoves);
            }
            NOfRepeatsFontSize = new ObservableType<int>(32);
            NOfRepeatsInt = new ObservableType<int>(1);
        }
        // attr
        private int ID = 2;
        public ObservableType<int> NOfRepeatsFontSize { get; set; }
        public ObservableType<int> NOfRepeatsInt { get; set; }

        public void HandlePageChanged(int msg)
        {
            if (msg != ID)
                return;
            Target.Instance.NumberOfMoves = NOfRepeatsInt.Value;
        }
        // handle drag&drop
        public void DragDropFile(string filename, string path)
        {
            Target.Instance.ConfigFileName = filename;
            Target.Instance.ConfigFilePath = path;
            return;
        }
        // icommand
        private ICommand? _ChooseFileCommand;
        private ICommand? _OpenMVGCommand;
        public ICommand ChooseFileCommand
        {
            get
            {
                if (_ChooseFileCommand == null)
                {
                    _ChooseFileCommand = new JSRelayCommand(
                        param => this.ChooseFile()
                    );
                }
                return _ChooseFileCommand;
            }
        }
        public ICommand OpenMVGCommand
        {
            get
            {
                if (_OpenMVGCommand == null)
                {
                    _OpenMVGCommand = new JSRelayCommand(
                        param => this.OpenMVG()
                    );
                }
                return _OpenMVGCommand;
            }
        }
        // command
        void ChooseFile()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                string src = openFileDlg.SelectedPath;
                if (src == null || src.Length == 0)
                {
                    return;
                }
                Target.Instance.ConfigFileName = System.IO.Path.GetFileName(src);
                Target.Instance.ConfigFilePath = src;
            }
        }
        void OpenMVG()
        {
            Target.Instance.NotifyChangeWindowToMVG();
        }
        public void RefreshIntegerUpDown(int msg) => Target.Instance.NumberOfMoves = msg;
    }
}
