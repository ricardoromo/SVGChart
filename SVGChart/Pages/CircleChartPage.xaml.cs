using System;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;
using SVGChart.Charts;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class CircleChartPage : ContentPage
    {
        CircleChart circleChart;
        //Percentage and HexColor
        private List<Tuple<int, string>> chartSegments;

        public CircleChartPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            InitChartValue();
            InitChart(350);
        }


        private void InitChart(double size)
        {
            skiaCanvas.HeightRequest = size;
            skiaCanvas.WidthRequest = size;
            skiaCanvas.InvalidateSurface();
        }

        public void InitChartValue()
        {

            chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(15, "#ff8787"),
                new Tuple<int, string>(10, "#42c0ff"),
                new Tuple<int, string>(25, "#b5f547"),
                new Tuple<int, string>(18, "#ffa14f"),
                new Tuple<int, string>(5, "#ff4281"),
            };

            circleChart = new CircleChart(chartSegments);
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.circleChart.DrawPictureAndFit(e);
        }

    }
}
