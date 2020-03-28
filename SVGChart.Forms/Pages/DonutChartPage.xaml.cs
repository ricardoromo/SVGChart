using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class DonutChartPage : ContentPage
    {
        public DonutChartPage()
        {
            InitializeComponent();
            InitChartValue();
        }

        public void InitChartValue()
        {

            var chartSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(15, "#ff8787"),
                new Tuple<int, string>(10, "#42c0ff"),
                new Tuple<int, string>(25, "#b5f547"),
                new Tuple<int, string>(18, "#ffa14f"),
                new Tuple<int, string>(5, "#ff4281"),
            };

            this.BindingContext = chartSegments;
        }
    }
}
