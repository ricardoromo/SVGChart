using System.Collections.Specialized;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class DonutChartView : BaseChartView
    {
        #region Bindable Properties
        public static readonly BindableProperty ChartTitleProperty =
            BindableProperty.Create(nameof(ChartTitle), typeof(string), typeof(DonutChartView), string.Empty, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty RingColorProperty =
             BindableProperty.Create(nameof(RingColor), typeof(Color), typeof(DonutChartView), Color.FromHex("#e6e6e6"),propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(DonutChartView), Color.White,propertyChanged: OnPropertyChanged); 

        public static readonly BindableProperty TitleColorProperty =
           BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(DonutChartView), Color.FromHex("#cccccc"),propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(nameof(StrokeWidth), typeof(int), typeof(DonutChartView),5, propertyChanged: OnPropertyChanged);

        public string ChartTitle
        {
            get { return (string)GetValue(ChartTitleProperty); }
            set { SetValue(ChartTitleProperty, value); }
        }

        public Color RingColor
        {
            get { return (Color)GetValue(RingColorProperty); }
            set { SetValue(RingColorProperty, value); }
        }

        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set { SetValue(TitleColorProperty, value); }
        }

        public int StrokeWidth
        {
            get { return (int)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }
        #endregion

        public DonutChartView() : base(ChartType.DonutChart)
        {
            Chart = new DonutChart();
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
           ((DonutChartView)bindable).OnPropertyChanged();
        }

        public override void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.Collection_CollectionChanged(sender, e);
            OnItemsSourceChanged();
        }

        public override void SetChartPorperties()
        {
            base.SetChartPorperties();
            (Chart as DonutChart).ChartTitle = ChartTitle;
            (Chart as DonutChart).StrokeWidth = StrokeWidth;
            (Chart as DonutChart).RingColor = RingColor;
            (Chart as DonutChart).FillColor = FillColor;
            (Chart as DonutChart).TitleColor = TitleColor;
         }
    }
}
