﻿using System;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;
using SVGChart.Charts;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class VerticalBarChartPage : ContentPage
    {
        VBarChart vBarChart;
        //Percentage and HexColor
        private List<Tuple<int, string>> chartSegments;

        public VerticalBarChartPage()
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
            skiaCanvas.HeightRequest = size;
            skiaCanvas.WidthRequest = size;
            skiaCanvas.InvalidateSurface();
        }

        public void InitChartValue()
        {
            chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(66, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
                new Tuple<int, string>(95, "#668cff"),
                new Tuple<int, string>(15, "#ffd633"),
            };

            vBarChart = new VBarChart(chartSegments);
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.vBarChart.DrawPictureAndFit(e);
        }
    }
}