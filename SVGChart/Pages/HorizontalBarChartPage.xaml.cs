using System;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;
using SVGChart.Charts;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class HorizontalBarChartPage : ContentPage
    {
        BarChart barChart;
        //Percentage and HexColor
        private List<Tuple<int, string>> chartSegments;
        public HorizontalBarChartPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            InitChartValue();
            InitChart();
        }


        private void InitChart()
        {
            skiaCanvas.HeightRequest = 330;
            skiaCanvas.WidthRequest = 370;
            skiaCanvas.InvalidateSurface();
        }

        public void InitChartValue()
        {
            chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(67, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(45, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
                new Tuple<int, string>(66, "#b5f547"),
                new Tuple<int, string>(100, "#ffa14f"),
            };

            barChart = new BarChart(chartSegments);
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.barChart.DrawPictureAndFit(e);
        }
    }
}
