using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class HorizontalBarChartPage : ContentPage
    {
        public HorizontalBarChartPage()
        {
            InitializeComponent();
            InitChartValue();
        }

        public void InitChartValue()
        {
            var chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(67, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(45, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
                new Tuple<int, string>(66, "#b5f547"),
                new Tuple<int, string>(100, "#ffa14f"),
                new Tuple<int, string>(67, "#ff8787"),
                new Tuple<int, string>(100, "#42c0ff"),
                new Tuple<int, string>(37, "#b5f547"),
                new Tuple<int, string>(45, "#ffa14f"),
                new Tuple<int, string>(10, "#40bf40"),
                new Tuple<int, string>(55, "#ff4d4d"),
            };

            this.BindingContext = chartSegments.AsEnumerable();
        }
    }
}
