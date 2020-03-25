using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using SkiaSharp.Extended.Svg;
using SkiaSharp.Views.Forms;
using SVGChart.Charts;
using SVGChart.Pages;
using Xamarin.Forms;

namespace SVGChart
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Circle_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DonutChartPage());
        }

        void HBar_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HorizontalBarChartPage());
        }

        void VBar_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VerticalBarChartPage());
        }

        void LinesBar_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LineChartPage());
        }

    }
}
