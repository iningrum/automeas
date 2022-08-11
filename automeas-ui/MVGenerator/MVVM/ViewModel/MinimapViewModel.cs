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
using CommunityToolkit;
using System.Windows.Interop;
using CommunityToolkit.Mvvm.Input;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal class MinimapViewModel
    {
        public readonly ObservableCollection<ObservablePoint> _observableValues;
        // ctor
        public MinimapViewModel()
        {
            MVGTarget.Instance.DataModified += HandleDataChanged;
            MVGTarget.Instance.FocusChanged += HandleFocusChanged;
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
        public bool _editMode = false;
        public ObservableCollection<ISeries> Series { get; set; }

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            MinLimit = 0,
            MaxLimit = 1.2*10,
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

        public ObservableCollection<RectangularSection> Sections { get; set; } = new ObservableCollection<RectangularSection>
    {
        new RectangularSection
        {
            Xi = MVGTarget.Instance.Xa,
            Xj = MVGTarget.Instance.Xb,
            Fill = new SolidColorPaint { Color = SKColors.White.WithAlpha(20) }
        },
    };
        public void HandleFocusChanged(ObservablePoint P)
        {
            XAxes[0].MinLimit = 0;
            Sections[0].Xi = P.X - 5;
            Sections[0].Xj = P.X + 5;
            XAxes[0].MaxLimit = MVGTarget.Instance.CurrentMove.X.Max+5;
        }
        public void HandleDataChanged(int i, ObservablePoint P)
        {
            switch (i)
            {
                case -1:
                    _observableValues.Add(P);
                    break;
                case -2:
                    if (_observableValues.Count() <= 1) { return; }
                    _observableValues.RemoveAt(_observableValues.Count() - 1);
                    break;
                default:
                    if(i == 0) { return; }
                    _observableValues[i] = P;
                    break;
            }
        }
    }
}
