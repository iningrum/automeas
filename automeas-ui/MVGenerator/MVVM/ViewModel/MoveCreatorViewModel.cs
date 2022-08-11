using automeas_ui.MWM.Model;
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

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal class MoveCreatorViewModel
    {
        // ctor
        public MoveCreatorViewModel(bool push)
        {
            MVGTarget.Instance.FocusChanged += HandleFocusChanged;
            var data = new ObservableCollection<ObservablePoint>
            {
                new(0, 0)
            };

            Data = data;

            SeriesCollection = new ISeries[]
            {
                new StepLineSeries<ObservablePoint>
                {
                    Values = data,
                    Fill = null,
                    DataPadding = new LiveChartsCore.Drawing.LvcPoint(5,5)
                }
            };
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
        // pagination/ adjustable X-zoom
        private ICommand? _zoom;
        public ICommand Zoom
        {
            get
            {
                if (_zoom == null)
                {
                    _zoom = new JSRelayCommand(
                        param => this.XZoom(param)
                    );
                }
                return _zoom;
            }
        }
        private void XZoom(object value)
        {
            double zoom = int.Parse(value.ToString());
            delta = 10.0 / zoom;
            ReloadXaxis();
            return;
        }
        public void ReloadXaxis(double panning = 0.0)
        {
            if (Data.Count()==0 || (panning <0 && Data.Last().X < -1*panning)) { return; }
            var axis = XAxes[0];
            axis.MinLimit = Data.Last().X - 0.2 * delta + panning;
            axis.MaxLimit = axis.MinLimit + delta;
            axis.MinStep = 0.1 * delta;
            { // push to Target
                var T = MVGTarget.Instance;
                T.Xmin = (double)axis.MinLimit;
                T.Xmax = (double)axis.MaxLimit;
                axis = YAxes[0];
                T.Ymin = (double)axis.MinLimit;
                T.Xmax = (double)axis.MaxLimit;
                axis = XAxes[0];
                //T.CurrentSeries = Data;
                T.NotifyRangeChanged((double)axis.MaxLimit);
            }
        }
        public double delta = 10;
        public void HandleFocusChanged(double x, double width)
        {
            var axis = XAxes[0];
            axis.MinLimit = x - width;
            axis.MaxLimit = x + width;
        }
    }
}
