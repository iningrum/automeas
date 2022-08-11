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
            double? last_x = viewModel.Data.Last().X;
            if (last_x!=null) {
                if(x <= last_x || Math.Abs(x-(double)last_x)<=StepperMotorDriver.Instance.SmallestDelay)
                {
                    x = (double)last_x;
                    x += StepperMotorDriver.Instance.SmallestDelay;
                }
            }
            // and the Y coordinate in the second position
            var y = scaledPoint[1];
            y = StepperMotorDriver.Instance.QuantitizeVelocity(y);
            // finally add the new point to the data in our chart.
            var ClickedPoint = new ObservablePoint(x, y);
            viewModel.Data.Add(ClickedPoint);
            viewModel.ReloadXaxis();
            MVGTarget.Instance.NotifyMoveUpdated(ClickedPoint);

        }
    }
}
