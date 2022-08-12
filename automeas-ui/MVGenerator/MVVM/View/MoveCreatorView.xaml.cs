using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
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
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.WPF;
using automeas_ui.MVGenerator.MVVM.ViewModel;
using automeas_ui.MVGenerator.MVVM.Model;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Markup;
using CommunityToolkit.Mvvm.Input;
using automeas_ui.MWM.Model.Launcher;

namespace automeas_ui.MVGenerator.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy MoveCreatorView.xaml
    /// </summary>
    public partial class MoveCreatorView : UserControl
    {
        public MoveCreatorView()
        {
            InitializeComponent();
        }

        private void Chart_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var chart = (CartesianChart)FindName("chart");
            var viewModel = (MoveCreatorViewModel)DataContext;

            // gets the point in the UI coordinates.
            var p = e.GetPosition(chart);

            // scales the UI coordinates to the corresponding data in the chart.
            // ScaleUIPoint returns an array of double
            var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));
            // where the X coordinate is in the first position
            var x = scaledPoint[0];
            // and the Y coordinate in the second position
            var y = scaledPoint[1];
            y = StepperMotorDriver.Instance.QuantitizeVelocity(y);
            if (MVGTarget.Instance._creator_EditMode == false)
            {
                double? last_x = viewModel.Data.Last().X;
                if (last_x != null)
                {
                    if (x <= last_x || Math.Abs(x - (double)last_x) <= StepperMotorDriver.Instance.SmallestDelay)
                    {
                        x = (double)last_x;
                        x += StepperMotorDriver.Instance.SmallestDelay;
                    }
                }

                // finally add the new point to the data in our chart.
                var ClickedPoint = new ObservablePoint(x, y);
                viewModel.Data.Add(ClickedPoint);
                MVGTarget.Instance.NotifyDataModified("+", ClickedPoint);
                MVGTarget.Instance.CurrentMove.X.Max = x;
                MVGTarget.Instance.NotifyFocusChanged(new ObservablePoint(x-2,x+5));
            }
            else
            {
                for(int i = 0; i < viewModel.Data.Count; i++)
                {
                    if (viewModel.Data[i].X > x)
                    {
                        if ( i>0)
                        {
                            if ((x - (double)viewModel.Data[i - 1].X) < ((double)viewModel.Data[i].X - x) && i>1)
                            {
                                viewModel.Data[i - 1].X = x;
                                viewModel.Data[i-1].Y = y;
                            }
                            else
                            {
                                viewModel.Data[i].X = x;
                                viewModel.Data[i].Y = y;
                            }
                        }
                        break;
                    }
                }
            }

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MVGTarget.Instance.NotifySave();
            { // load new move
                var viewModel = (MoveCreatorViewModel)DataContext;
                if (MVGTarget.Instance.CurrentMove.id == -1)
                {
                    if (viewModel._push == true)
                    {
                        MVGTarget.Instance.CurrentMove.id = 0;
                    }
                    else
                    {
                        MVGTarget.Instance.CurrentMove.id = 1;
                    }
                }
                MVGTarget.Instance.SaveCurrentMove();
            }
            MVGTarget.Instance.NotifyViewNavigate("main");
        }
    }
}
