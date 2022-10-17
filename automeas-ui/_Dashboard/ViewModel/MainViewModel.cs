using automeas_ui._Launcher.Model;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace automeas_ui._Dashboard.ViewModel
{
    internal partial class MainViewModel
    {
        [RelayCommand]
        void Return() => ((App)automeas_ui._Common.Navigator.App._handle).sSwitch("\r");
        public ObservableType<string> Title { get; set; }
        public ObservableType<string> Subtitle { get; set; }
        public ObservableType<string> EstimatedTime { get; set; }
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 20.15, 20.171, 20.143, 20.189, 20.149, 19.0, 20.14, 20.13, 20.0, 19.98, 19.97, 19.35, 19.2, 18, 20.15, 20.151, 20.153, 20.149, 20.149, 20.148, 20.1475, 20.13, 20.11, 20.5, 20.4, 20.1, 19, 19.1 },
                    Name = "",
                    GeometrySize=10
                },
                new LineSeries<double?>
                {
                    DataLabelsSize = 12,
                    DataLabelsPaint = new SolidColorPaint(SKColors.White),
                    DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                    // The DataLabelsFormatter is a function 
                    // that takes the current point as parameter
                    // and returns a string.
                    // in this case we returned the PrimaryValue property as currency
                    DataLabelsFormatter = (point) => point.PrimaryValue.ToString(""),
                    Values = new double?[] { 20.15, null, 20.143, null, 20.149, null, 20.14, null, 20.0, null, 19.97, null, 19.2, null, 20.15, null, 20.153, null, 20.149, null, 20.1475, null, 20.11, null, 20.4, null, 19, null },
                    Name = "",
                    Fill=null,
                    GeometryFill=null,
                    GeometryStroke=null,
                    Stroke=null
                },

            };
        public Axis[] XAxes { get; set; } =
            {
        new()
        {
            ForceStepToMin = true,
            MinStep = 0.5,
            TextSize = 14,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.Gray,
                StrokeThickness = 2
            }
        }
    };

        public MainViewModel()
        {
            Title = new("Profil B");
            Subtitle = new("Pomiar nr. 7");
            EstimatedTime = new("1h 30m 15s");
        }
        public Axis[] YAxes { get; set; } =
        {
        new()
        {
            MinLimit = 17.5,
            MaxLimit = 21.0,
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
    }
}
