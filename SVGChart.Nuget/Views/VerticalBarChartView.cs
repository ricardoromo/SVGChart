using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class VerticalBarChartView : BaseChartView
    {
        public static readonly BindableProperty DisplayValueProperty =
            BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(VerticalBarChartView), true);

        public static readonly BindableProperty BarWidthProperty =
            BindableProperty.Create(nameof(BarWidth), typeof(BarSize), typeof(VerticalBarChartView), BarSize.Medium);

        public bool DisplayValue
        {
            get { return (bool)GetValue(DisplayValueProperty); }
            set { SetValue(DisplayValueProperty, value); }
        }

        public BarSize BarWidth
        {
            get { return (BarSize)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }

        public VerticalBarChartView()
        {
            Chart = new VerticalBarChart();
        }

        public override void OnItemsSourceChanged()
        {
            base.OnItemsSourceChanged();
            (Chart as VerticalBarChart).DisplayValues = DisplayValue;
            (Chart as VerticalBarChart).BarWidth = (int)BarWidth;
            LoadChart(ChartType.VerticalBarChart);
        }
    }
}
