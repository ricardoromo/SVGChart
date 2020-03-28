using System.Collections.Generic;
using Xamarin.Forms;

namespace SVGChart.Pages
{
    public partial class LineChartPage : ContentPage
    {
        
        public LineChartPage()
        {
            InitializeComponent();
            InitChartValue();
        }

        public void InitChartValue()
        {
            var chartSegments = new List<int>()
            {
               66,34,88,20,90,55,65,80,45
            };

            this.BindingContext = chartSegments;
        }
    }
}
