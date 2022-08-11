﻿using automeas_ui.Core;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.Zoombox;

namespace automeas_ui.MVGenerator.MVVM.Model
{
    internal class MVGTarget
    {
        // singleton implementation - thread safe
        private static readonly Lazy<MVGTarget> lazy = new Lazy<MVGTarget>(() => new MVGTarget());
        public static MVGTarget Instance { get { return lazy.Value; } }
        // ctor
        public MVGTarget()
        {
            Xmin = 0;
            Xmax = 10;
            Ymin = 0;
            Ymax = 10;
        }
        public double Xmin, Ymin, Xmax, Ymax;
        private double[] PushSeries, PullSeries;
        public TrulyObservableCollection<ObservablePoint> CurrentSeries = new TrulyObservableCollection<ObservablePoint>();
        // event
        public event Action<ObservablePoint>? MoveUpdated;
        public event Action<double> RangeChanged;
        public void NotifyMoveUpdated(ObservablePoint msg) => MoveUpdated?.Invoke(msg);
        public void NotifyRangeChanged(double max) => RangeChanged?.Invoke(max);

    }
}
