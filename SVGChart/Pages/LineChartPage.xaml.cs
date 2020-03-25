using System;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;
using SVGChart.Charts;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class LineChartPage : ContentPage
    {
        LineChart lineChart;
        private List<int> chartSegments;

        public LineChartPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            InitChartValue();
            InitChart(width);
        }

        private void InitChart(double size)
        {
            skiaCanvas.HeightRequest = 350;
            skiaCanvas.WidthRequest = 400;
            skiaCanvas.InvalidateSurface();
        }

        public void InitChartValue()
        {
            chartSegments = new List<int>()
            {
               66,34,88,20,90,55,65,80,45
            };

            lineChart = new LineChart(chartSegments, Color.FromHex("#668cff"));
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.lineChart.DrawPictureAndFit(e);
        }
    }
}
