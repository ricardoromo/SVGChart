using System;
using System.Collections.Generic;
using SVGChart.Nuget.Charts;
using SVGChart.Nuget.Utils;
using Xamarin.Forms;

namespace SVGChart.Nuget.Views
{
    public class DonutChartView : BaseChartView
    {

        public static readonly BindableProperty CharTitleProperty =
            BindableProperty.Create(nameof(CharTitle), typeof(string), typeof(DonutChartView), string.Empty);

        public static readonly BindableProperty RingColorProperty =
             BindableProperty.Create(nameof(RingColor), typeof(Color), typeof(DonutChartView), Color.FromHex("#e6e6e6"));

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(DonutChartView), Color.White);

        public static readonly BindableProperty TitleColorProperty =
           BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(DonutChartView), Color.FromHex("#cccccc"));

        public static readonly BindableProperty StrokeWidthProperty =
            BindableProperty.Create(nameof(StrokeWidth), typeof(int), typeof(DonutChartView),5);

        public string CharTitle
        {
            get { return (string)GetValue(CharTitleProperty); }
            set { SetValue(CharTitleProperty, value); }
        }

        public Color RingColor
        {
            get { return (Color)GetValue(RingColorProperty); }
            set { SetValue(RingColorProperty, value); }
        }

        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set { SetValue(TitleColorProperty, value); }
        }

        public int StrokeWidth
        {
            get { return (int)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public DonutChartView()
        {
            Chart = new DonutChart();
        }

        public override void OnItemsSourceChanged()
        {
            base.OnItemsSourceChanged();
            (Chart as DonutChart).CharTitle = CharTitle;
            (Chart as DonutChart).StrokeWidth = StrokeWidth;
            (Chart as DonutChart).RingColor = RingColor;
            (Chart as DonutChart).FillColor = FillColor;
            (Chart as DonutChart).TitleColor = TitleColor;
            LoadChart(ChartType.DonutChart);
        }
    }
}
