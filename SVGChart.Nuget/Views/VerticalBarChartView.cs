using System.Collections.Specialized;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class VerticalBarChartView : BaseChartView
    {
        #region Bindable Properties
        public static readonly BindableProperty DisplayValueProperty =
            BindableProperty.Create(nameof(DisplayValue), typeof(bool), typeof(VerticalBarChartView), true, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty BarWidthProperty =
            BindableProperty.Create(nameof(BarWidth), typeof(BarSize), typeof(VerticalBarChartView), BarSize.Medium, propertyChanged: OnPropertyChanged);

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
        #endregion

        public VerticalBarChartView() : base(ChartType.VerticalBarChart)
        {
            Chart = new VerticalBarChart();
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
            ((VerticalBarChartView)bindable).OnPropertyChanged();
        }

        public override void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.Collection_CollectionChanged(sender, e);
            OnItemsSourceChanged();
        }

        public override void SetChartPorperties()
        {
            base.SetChartPorperties();
            (Chart as VerticalBarChart).DisplayValues = DisplayValue;
            (Chart as VerticalBarChart).BarWidth = (int)BarWidth;
        }
    }
}
