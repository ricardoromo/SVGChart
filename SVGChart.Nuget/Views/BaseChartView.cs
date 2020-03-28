using System.Collections;
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
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(DonutChartView), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public BaseChart Chart { get; set; }

        private SKCanvasView canvasView;
        public BaseChartView()
        {
            LoadCanvasView();
            InitHandlers();
        }

        private void LoadCanvasView()
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

        public virtual void LoadChart(ChartType chartType)
        {
            canvasView.HeightRequest = this.HeightRequest;
            canvasView.WidthRequest = this.WidthRequest;
            Chart.LoadSvg(chartType);
            canvasView.InvalidateSurface();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            Chart.DrawPictureAndFit(e);
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((BaseChartView)bindable).OnItemsSourceChanged();
        }

        public virtual void OnItemsSourceChanged()
        {
            Chart.Segments = ItemSource;
        }
    }
}
