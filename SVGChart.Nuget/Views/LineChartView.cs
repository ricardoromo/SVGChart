using System;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class LineChartView : BaseChartView
    {
        public static readonly BindableProperty LineColorProperty =
           BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(LineChartView), Color.DodgerBlue);

        public static readonly BindableProperty PointsColorProperty =
           BindableProperty.Create(nameof(PointsColor), typeof(Color), typeof(LineChartView), Color.Red);

        public static readonly BindableProperty PointsDistanceProperty =
          BindableProperty.Create(nameof(PointsDistance), typeof(double), typeof(LineChartView), 12.0);

        public static readonly BindableProperty PointSizeProperty =
          BindableProperty.Create(nameof(PointSize), typeof(double), typeof(LineChartView), 2.0);

        public static readonly BindableProperty LineWidthProperty =
          BindableProperty.Create(nameof(LineWidth), typeof(double), typeof(LineChartView), 1.0);

        public static readonly BindableProperty ShowPointsProperty =
          BindableProperty.Create(nameof(ShowPoints), typeof(bool), typeof(LineChartView), true);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public Color PointsColor
        {
            get { return (Color)GetValue(PointsColorProperty); }
            set { SetValue(PointsColorProperty, value); }
        }

        public double PointsDistance
        {
            get { return (double)GetValue(PointsDistanceProperty); }
            set { SetValue(PointsDistanceProperty, value); }
        }

        public double PointSize
        {
            get { return (double)GetValue(PointSizeProperty); }
            set { SetValue(PointSizeProperty, value); }
        }

        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        public bool ShowPoints
        {
            get { return (bool)GetValue(ShowPointsProperty); }
            set { SetValue(ShowPointsProperty, value); }
        }

        public LineChartView()
        {
            Chart = new LineChart();
        }

        public override void OnItemsSourceChanged()
        {
            base.OnItemsSourceChanged();
            (Chart as LineChart).LineColor = LineColor;
            (Chart as LineChart).PointsColor = PointsColor;
            (Chart as LineChart).PointSize = PointSize;
            (Chart as LineChart).PointsDistance = PointsDistance;
            (Chart as LineChart).LineWidth = LineWidth;
            (Chart as LineChart).ShowPoints = ShowPoints;
            LoadChart(ChartType.LineChart);
        }
    }
}
