using System.Collections.Specialized;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class HorizontalBarChartView : BaseChartView
    {
        #region Bindable Properties
        public static readonly BindableProperty DisplayValueProperty =
            BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(HorizontalBarChartView), true, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty BarHeightProperty =
          BindableProperty.Create(nameof(BarHeight), typeof(BarSize), typeof(HorizontalBarChartView), BarSize.Medium, propertyChanged: OnPropertyChanged);

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
        #endregion

        public HorizontalBarChartView() : base(ChartType.HorizontalBarChart)
        {
            Chart = new HorizontalBarChart();
        }

        public override void OnItemsSourceChanged()
        {
            base.OnItemsSourceChanged();
            SetChartPorperties();
            LoadChart();
        }

        public override void OnPropertyChanged()
        {
            SetChartPorperties();
            base.OnPropertyChanged();
        }

        static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((HorizontalBarChartView)bindable).OnPropertyChanged();
        }

        public override void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.Collection_CollectionChanged(sender, e);
            OnItemsSourceChanged();
        }

        public override void SetChartPorperties()
        {
            base.SetChartPorperties();
            (Chart as HorizontalBarChart).DisplayValues = DisplayValue;
            (Chart as HorizontalBarChart).BarHeight = (int)BarHeight;
        }
    }
}
