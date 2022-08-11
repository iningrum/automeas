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
using System.Collections.ObjectModel;
using automeas_ui.MVGenerator.MVVM.Model;
using LiveChartsCore.Defaults;
using automeas_ui.Core;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal class MinimapViewModel
    {
        private readonly ObservableCollection<ObservablePoint> _observableValues;
        // ctor
        public MinimapViewModel()
        {
            MVGTarget.Instance.MoveUpdated += HandlePointAdded;
            _observableValues = new ObservableCollection<ObservablePoint> { new ObservablePoint(0,0)};
            Series = new ObservableCollection<ISeries>
            {
                new StepLineSeries<ObservablePoint>
                {
                    Values = _observableValues,
                    Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 1.5F },
                    Fill=null,
                    GeometryFill=null,
                    GeometryStroke=null,
                }
            };
        }
        public ObservableCollection<ISeries> Series { get; set; }

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            MinLimit = MVGTarget.Instance.Xmin,
            MaxLimit = MVGTarget.Instance.Xmax,
            ForceStepToMin = true,
            MinStep = int.MaxValue,
            TextSize = 0,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.Gray,
                StrokeThickness = 2
            }
        }
    };

        public Axis[] YAxes { get; set; } =
        {
        new Axis
        {
            MinLimit = 0,
            MaxLimit = 10,
            ForceStepToMin = true,
            MinStep = int.MaxValue,
            TextSize = 0,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.Gray,
                StrokeThickness = 2,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            }
        }
    };

        public DrawMarginFrame Frame { get; set; } =
        new()
        {
            Fill = new SolidColorPaint
            {
                Color = new SKColor(0, 0, 0, 30)
            },
            Stroke = new SolidColorPaint
            {
                Color = new SKColor(80, 80, 80),
                StrokeThickness = 2
            }
        };
        public void HandlePointAdded(ObservablePoint msg)
        {
            _observableValues.Add(msg);
        }
    }
}
