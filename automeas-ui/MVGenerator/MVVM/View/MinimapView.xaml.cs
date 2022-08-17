using automeas_ui.MVGenerator.MVVM.Model;
using automeas_ui.MVGenerator.MVVM.ViewModel;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using System.Windows.Controls;
using System.Windows.Input;

namespace automeas_ui.MVGenerator.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy MinimapView.xaml
    /// </summary>
    public partial class MinimapView : UserControl
    {
        public MinimapView()
        {
            InitializeComponent();
        }

        private void chart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var chart = (CartesianChart)FindName("chart");
            var viewModel = (MinimapViewModel)DataContext;

            // gets the point in the UI coordinates.
            var p = e.GetPosition(chart);

            // scales the UI coordinates to the corresponding data in the chart.
            // ScaleUIPoint returns an array of double
            var scaledPoint = chart.ScaleUIPoint(new LvcPoint((float)p.X, (float)p.Y));
            // where the X coordinate is in the first position
            var x = scaledPoint[0];
            // and the Y coordinate in the second position
            var y = scaledPoint[1];
            // finally add the new point to the data in our chart.
            var ClickedPoint = new ObservablePoint(x, y);
            MVGTarget.Instance.NotifyFocusChanged(ClickedPoint);
        }
        private void chart_RMBDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (MinimapViewModel)DataContext;
            if (MVGTarget.Instance._creator_EditMode == true) { return; }
            else { MVGTarget.Instance._creator_EditMode = true; }
            viewModel.Series.RemoveAt(0);
            viewModel.Series.Add(new StepLineSeries<ObservablePoint>
            {
                Values = viewModel._observableValues,
                Stroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 1.5F },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
            });
            //viewModel.Series.RemoveAt(0);
        }
        private void chart_LMBDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (MinimapViewModel)DataContext;
            if (MVGTarget.Instance._creator_EditMode == false) { return; }
            else { MVGTarget.Instance._creator_EditMode = false; }
            viewModel.Series.RemoveAt(0);
            viewModel.Series.Add(new StepLineSeries<ObservablePoint>
            {
                Values = viewModel._observableValues,
                Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 1.5F },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
            });
            //viewModel.Series.RemoveAt(0);
        }
    }
}
