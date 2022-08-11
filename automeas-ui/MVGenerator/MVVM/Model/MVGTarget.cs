using automeas_ui.Core;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.Zoombox;

namespace automeas_ui.MVGenerator.MVVM.Model
{
    internal partial class MVGTarget
    {
        // singleton implementation - thread safe
        private static readonly Lazy<MVGTarget> lazy = new Lazy<MVGTarget>(() => new MVGTarget());
        public static MVGTarget Instance { get { return lazy.Value; } }
        // ctor
        public MVGTarget()
        {
        }
        private List<MVData> Moves = new List<MVData>()
        {
            new MVData(),
            new()
        };
        public MVData CurrentMove = new MVData();
        public bool _creator_EditMode = false;
        public TrulyObservableCollection<ObservablePoint> CurrentSeries = new TrulyObservableCollection<ObservablePoint>();
        // event
        public event Action<ObservablePoint> FocusChanged;
        public event Action<int, ObservablePoint> DataModified;
        public void NotifyFocusChanged(ObservablePoint P)
        {
            FocusChanged?.Invoke(P);
        }
        public void NotifyFocusChanged(double a, double b)
        {
            FocusChanged?.Invoke(new ObservablePoint(a, b));
        }
        public void NotifyDataModified(string code, ObservablePoint value, int i = 0)
        {
            switch (code)
            {
                case "+":
                    DataModified?.Invoke(-1, value);
                    break;
                case "-":
                    DataModified?.Invoke(-2, value);
                    break;
                case "e":
                    DataModified?.Invoke(i, value);
                    break;
            }

        }

    }
    internal partial class MVGTarget
    {
        public struct MVData
        {
            public MVData()
            {
                X = new(0, 10);
                Y = new(0, 10);
                Focus = new Axis(0, 10);
                Data = new ObservableCollection<ObservablePoint>
                {
                    new ObservablePoint(0,0)
                };
            }
            public MVData(ObservableCollection<ObservablePoint> data)
            {
                X = new(0, 10);
                Y = new(0, 10);
                Focus = new Axis(0, 10);
                Data = data;
            }
            public Axis X, Y, Focus;
            public ObservableCollection<ObservablePoint> Data;
        }
        public struct Axis
        {
            public double Min, Max;
            public Axis(double min, double max){Min = min; Max = max; } 
        }
    }
}
