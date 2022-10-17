using automeas_ui.Core;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._MVG.Model
{
    internal partial class MVGTarget
    {
        // singleton implementation - thread safe
        private static readonly Lazy<MVGTarget> lazy = new(() => new());
        public static MVGTarget Instance { get { return lazy.Value; } }
        // ctor
        public MVGTarget()
        {
        }
        private List<MVData> Moves = new()
        {
            new(),
            new()
        };
        public MVData CurrentMove = new();
        public bool _creator_EditMode = false;
        public TrulyObservableCollection<ObservablePoint> CurrentSeries = new();
        // event
        public event Action<ObservablePoint> FocusChanged;
        public event Action<string, int, ObservablePoint> UndoRedoPerformed;
        public event Action Save;
        public event Action<string> ViewNavigate;
        public event Action<int, ObservablePoint> DataModified;
        public void NotifyUndoRedoPerformed(string action, int id, ObservablePoint P) => UndoRedoPerformed?.Invoke(action, id, P);
        public void NotifyFocusChanged(ObservablePoint P)
        {
            FocusChanged?.Invoke(P);
        }
        public void NotifyFocusChanged(double a, double b)
        {
            FocusChanged?.Invoke(new(a, b));
        }
        public void NotifySave() => Save?.Invoke();
        public void NotifyViewNavigate(string id)
        {
            ViewNavigate?.Invoke(id);
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
        public void SaveCurrentMove(int new_id = 0)
        {
            if (CurrentMove.id == -1) { return; }
            else if (CurrentMove.id < 0 || CurrentMove.id > Moves.Count - 1) { throw new InvalidOperationException("Id has been set but is invalid"); }
            else
            {
                Moves[CurrentMove.id] = CurrentMove; // load Move into the list
                CurrentMove = Moves[new_id]; // grab new move from saved moves list
                CurrentMove.id = new_id;
            }
        }
        public void LoadCurrentMove(int id)
        {
            CurrentMove = Moves[id];
        }
        public struct MVData
        {
            public int id = -1;
            public MVData()
            {
                X = new(0, 10);
                Y = new(0, 10);
                Focus = new(0, 10);
                Data = new()
                {
                    new(0,0)
                };
            }
            public MVData(ObservableCollection<ObservablePoint> data)
            {
                X = new(0, 10);
                Y = new(0, 10);
                Focus = new(0, 10);
                Data = data;
            }
            public Axis X, Y, Focus;
            public ObservableCollection<ObservablePoint> Data;
        }
        public struct Axis
        {
            public double Min, Max;
            public Axis(double min, double max) { Min = min; Max = max; }
        }
    }
}
