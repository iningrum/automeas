using automeas_ui.MVGenerator.MVVM.Model;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace automeas_ui.MVGenerator.MVVM.ViewModel
{
    internal partial class MinimapViewModel
    {
        public readonly ObservableCollection<ObservablePoint> _observableValues;
        public List<URSave> SaveBuffer = new();
        // ctor
        public MinimapViewModel()
        {
            var mvgt = MVGTarget.Instance.CurrentMove;
            MVGTarget.Instance.DataModified += HandleDataChanged;
            MVGTarget.Instance.FocusChanged += HandleFocusChanged;
            MVGTarget.Instance.Save += Save;
            _observableValues = new() { new(0, 0) };
            Series = new()
            {
                new StepLineSeries<ObservablePoint>
                {
                    Values = (mvgt.Data.Count()>1)?  mvgt.Data: _observableValues,
                    Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 1.5F },
                    Fill=null,
                    GeometryFill=null,
                    GeometryStroke=null,
                }
            };
            { // load from MVGT
                if (mvgt.Data.Count() > 1)
                {
                    // assign data
                    _observableValues = mvgt.Data;
                    // setting data range
                    XAxes[0].MinLimit = 0;
                    XAxes[0].MaxLimit = mvgt.X.Max + 5;
                    // Drawing rectangle
                    Sections[0].Xi = mvgt.Focus.Min;
                    Sections[0].Xj = mvgt.Focus.Max;
                }
            }
        }
        ~MinimapViewModel()
        {
            MVGTarget.Instance.DataModified -= HandleDataChanged;
            MVGTarget.Instance.FocusChanged -= HandleFocusChanged;
            { // upload to MVGT
                var mvgt = MVGTarget.Instance.CurrentMove;
                mvgt.X = new((double)XAxes[0].MinLimit, (double)XAxes[0].MaxLimit);
            }
            MVGTarget.Instance._creator_EditMode = false;
        }
        public void Save()
        {
            { // upload to MVGT
                var mvgt = MVGTarget.Instance.CurrentMove;
                mvgt.X = new((double)XAxes[0].MinLimit, (double)XAxes[0].MaxLimit);
                MVGTarget.Instance.CurrentMove = mvgt;
            }
            MVGTarget.Instance._creator_EditMode = false;
        }
        public bool _editMode = false;
        public ObservableCollection<ISeries> Series { get; set; }

        public Axis[] XAxes { get; set; } =
        {
        new()
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
        new()
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
                Color = new(0, 0, 0, 30)
            },
            Stroke = new SolidColorPaint
            {
                Color = new(80, 80, 80),
                StrokeThickness = 2
            }
        };

        public ObservableCollection<RectangularSection> Sections { get; set; } = new()
        {
        new()
        {
            Xi = null,
            Xj = null,
            Fill = new SolidColorPaint { Color = SKColors.White.WithAlpha(20) }
        },
    };
        public void HandleFocusChanged(ObservablePoint P)
        {
            XAxes[0].MinLimit = 0;
            Sections[0].Xi = P.X - 5;
            Sections[0].Xj = P.X + 5;
            XAxes[0].MaxLimit = MVGTarget.Instance.CurrentMove.X.Max + 5;
        }
        public void HandleDataChanged(int i, ObservablePoint P)
        {
            switch (i)
            {
                case -1:
                    if (_observableValues.Last() == P) { return; } // hotfix for visual bug caused by double event trigger
                    SaveBuffer.Add(new(_observableValues.Count(), "+", P));
                    _observableValues.Add(P);
                    break;
                case -2:
                    if (_observableValues.Count() <= 1) { return; }
                    //SaveBuffer.Add(new(_observableValues.Count() - 1, "-", P));
                    _observableValues.RemoveAt(_observableValues.Count() - 1);
                    break;
                default:
                    if (i == 0) { return; }
                    //SaveBuffer.Add(new(i, "e", _observableValues[i]));
                    _observableValues[i] = P;
                    break;
            }
            if (SaveBuffer.Count > 6)
            {
                SaveBuffer.RemoveAt(0);

            }
        }
        // commands
        [RelayCommand]
        public void Undo()
        {
            if (SaveBuffer.Count() < 1) { return; }
            var action = SaveBuffer.Last();
            switch (action.Type)
            {
                case "+":
                    action.Type = "-";
                    MVGTarget.Instance.NotifyUndoRedoPerformed(action.Type, action.Index, action.Point);
                    if (_observableValues.Count() - 1 > 0)
                    {
                        _observableValues.RemoveAt(_observableValues.Count() - 1);
                        SaveBuffer.RemoveAt(SaveBuffer.Count() - 1);
                    }
                    break;
                case "e":
                    break;
                default:
                    break;
            }
        }
        [RelayCommand]
        public void Redo()
        {

        }
        public struct URSave
        {
            public int Index;
            public string Type;
            public ObservablePoint Point;
            public URSave(int i, string t, ObservablePoint point)
            {
                Point = point;
                Index = i;
                Type = t;
            }
        }
    }
}
