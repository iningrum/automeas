﻿using automeas_ui.MWM.Model;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using automeas_ui.MWM.Model.Launcher;
using automeas_ui.Core;
using automeas_ui.MVGenerator.MVVM.Model;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal partial class MoveCreatorViewModel
    {
        // ctor
        public MoveCreatorViewModel(bool push)
        {
            MVGTarget.Instance.FocusChanged += HandleFocusChanged;
            var mvgt = MVGTarget.Instance.CurrentMove;
            _push = push;
            if (mvgt.Data.Count() > 1)
            {
                Data = mvgt.Data;
                XAxes[0].MinLimit = mvgt.Focus.Min;
                XAxes[0].MaxLimit = mvgt.Focus.Max;
            }
            else
            {
                var data = new ObservableCollection<ObservablePoint>
                {
                    new(0, 0)
                };

                Data = data;
            }
            

            SeriesCollection = new ISeries[]
            {
                new StepLineSeries<ObservablePoint>
                {
                    Values = Data,
                    Fill = null,
                    DataPadding = new LiveChartsCore.Drawing.LvcPoint(5,5)
                }
            };
        }
        ~MoveCreatorViewModel()
        {
            MVGTarget.Instance.FocusChanged -= HandleFocusChanged;
            { // upload data to MVGT
                var mvgt = MVGTarget.Instance.CurrentMove;
                mvgt.Focus = new((double)XAxes[0].MinLimit, (double)XAxes[0].MaxLimit);
                mvgt.Data = Data;
                
            }
        }
        [RelayCommand]
        public void Save()
        {
            var mvgt = MVGTarget.Instance.CurrentMove;
            mvgt.Focus = new((double)XAxes[0].MinLimit, (double)XAxes[0].MaxLimit);
            mvgt.Data = Data;
            MVGTarget.Instance.CurrentMove = mvgt;
        }
        // lvc
        public ObservableCollection<ObservablePoint> Data { get; set; }
        public ISeries[] SeriesCollection { get; set; }
        public Axis[] YAxes { get; set; } =
        {
        new Axis
        {
            MinLimit = 0,
            MaxLimit = 10,
            ForceStepToMin = true,
            MinStep = 1,
            TextSize = 14,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.Gray,
                StrokeThickness = 2,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            }
        }
    };
        public Axis[] XAxes { get; set; } =
            {
        new Axis
        {
            MinLimit = 0,
            MaxLimit = 10,
            ForceStepToMin = true,
            MinStep = 1,
            TextSize = 14,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.Gray,
                StrokeThickness = 2,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            }
        }
    };
        private readonly bool _push;
        public RectangularSection[] Sections { get; set; } = GenerateLineSections();
        private static RectangularSection[] GenerateLineSections()
        {
            var result = new RectangularSection[StepperMotorDriver.Instance.Step.Count()];
            for (int i= 0; i < StepperMotorDriver.Instance.Step.Count(); i++)
            {
                result[i] = new RectangularSection
                {
                    Yi = StepperMotorDriver.Instance.Step[i],
                    Yj = StepperMotorDriver.Instance.Step[i],
                    Stroke = new SolidColorPaint
                    {
                        Color = SKColors.Black,
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 6, 6 })
                    }
                };
            }
            return result;
        }
        public void HandleFocusChanged(ObservablePoint P)
        {
            if (P.X - 5 > 0)
            {
                XAxes[0].MinLimit = P.X - 5;
            }
            else
            {
                XAxes[0].MinLimit = 0;
            }
            XAxes[0].MaxLimit = P.X+5;
        }
    }
}
