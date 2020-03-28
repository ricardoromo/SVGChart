using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class HorizontalBarChartView : BaseChartView
    {
        public static readonly BindableProperty DisplayValueProperty =
            BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(HorizontalBarChartView), true);

        public static readonly BindableProperty BarHeightProperty =
          BindableProperty.Create(nameof(BarHeight), typeof(BarSize), typeof(HorizontalBarChartView), BarSize.Medium);

        public bool DisplayValue
        {
            get { return (bool)GetValue(DisplayValueProperty); }
            set { SetValue(DisplayValueProperty, value); }
        }

        public BarSize BarHeight
        {
            get { return (BarSize)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }


        public HorizontalBarChartView()
        {
            Chart = new HorizontalBarChart();
        }

        public override void OnItemsSourceChanged()
        {
            base.OnItemsSourceChanged();
            (Chart as HorizontalBarChart).DisplayValues = DisplayValue;
            (Chart as HorizontalBarChart).BarHeight = (int)BarHeight;
            LoadChart(ChartType.HorizontalBarChart);
        }
    }
}
