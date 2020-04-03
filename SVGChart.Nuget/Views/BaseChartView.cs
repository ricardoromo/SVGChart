using System;
using System.Collections;
using System.Collections.Specialized;
using SkiaSharp.Views.Forms;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Extension;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class BaseChartView : ContentView 
    {
        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(BaseChartView), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        protected BaseChart Chart { get; set; }
        protected private SKCanvasView canvasView;
        protected private ChartType chartType;

        public BaseChartView(ChartType charType)
        {
            this.chartType = charType;
            InitCanvasView();
            InitHandlers();
        }

        private void InitCanvasView()
        {
            canvasView = new SKCanvasView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            this.Content = canvasView;
        }

        private void InitHandlers()
        {
            canvasView.PaintSurface -= OnCanvasViewPaintSurface;
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            Chart.DrawPictureAndFit(e);
        }


        public virtual void OnPropertyChanged()
        {
            LoadChart();
        }

        public virtual void LoadChart()
        {
            Chart.LoadSvg(chartType);
            canvasView.InvalidateSurface();
        }

        public virtual void SetChartPorperties()
        {
            canvasView.HeightRequest = this.HeightRequest;
            canvasView.WidthRequest = this.WidthRequest;
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((BaseChartView)bindable).OnItemsSourceChanged();
        }

        public virtual void OnItemsSourceChanged()
        {
            Chart.Segments = ItemSource;
            AddCollectionChanged(ItemSource);
        }

        private void AddCollectionChanged(IEnumerable list)
        {
            if (list is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged -= Collection_CollectionChanged; ;
                collection.CollectionChanged += Collection_CollectionChanged; ;
            }
        }

        public virtual void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }
    }
}
