using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class VerticalBarChartPage : ContentPage
    {
        public VerticalBarChartPage()
        {
            InitializeComponent();
            InitChartValue();
        }

        public void InitChartValue()
        {
            var chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(66, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
                new Tuple<int, string>(95, "#668cff"),
                new Tuple<int, string>(15, "#ffd633"),
                new Tuple<int, string>(86, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(66, "#ffa14f"),

            };

           this.BindingContext = chartSegments;
        }
    }
}
