using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using SkiaSharp.Extended.Svg;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SVGChart
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string defaultRingColor = "#e6e6e6";
        private float strokeWidth = 4;
        private SKSvg svg;
        private double CharSize;

        //Percentage and HexColor
        private List<Tuple<int, string>> CharSegments;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CharSegments = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(15, "#ff8787"),
                new Tuple<int, string>(10, "#42c0ff"),
                new Tuple<int, string>(25, "#b5f547"),
                new Tuple<int, string>(18, "#ffa14f"),
                new Tuple<int, string>(5, "#ff4281"),
            };
            CharSize = 300;
            InitChart();
        }

        private void InitChart()
        {
            if (CharSize < 1)
                return;

            var size = CharSize - 30;
            skiaCanvas.HeightRequest = size;
            skiaCanvas.WidthRequest = size;

            svg = null;
            LoadSvg();
            skiaCanvas.InvalidateSurface();
        }

        private void LoadSvg()
        {
            this.svg = new SKSvg();
            this.svg.LoadAsXMLdocument<MainPage>("SVGChart.Resources.Chart.svg", document =>
            {
                var elements = CharSegments;
                if (elements == null)
                    return;
                UpdateSvg(document, elements);
            });
        }

        private void UpdateSvg(XmlDocument document, IEnumerable<Tuple<int, string>> elements)
        {
            var offset = 25;
            var previousOffset = -1;
            var previousSpace = -1;
            var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

            var ringNode = root.GetElementsByTagName("circle").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-ring");

            var ringAttr = ringNode.Attributes;
            ringAttr["stroke-width"].Value = strokeWidth.ToString();
            ringAttr["stroke"].Value = defaultRingColor;

            var segmentNodeToCopy = root.GetElementsByTagName("circle").Cast<XmlElement>()
                           .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-segment");


            foreach (var el in elements)
            {
                if (el.Item1 < 0)
                    continue;

                var newnode = segmentNodeToCopy.CloneNode(true);
                var attr = newnode.Attributes;
                attr["stroke-width"].Value = strokeWidth.ToString();
                attr["stroke"].Value = el.Item2;

                var spaceDash = 100 - el.Item1;
                attr["stroke-dasharray"].Value = $"{ el.Item1 }, { spaceDash }";

                var newOffset = (previousOffset < 0) ? offset : (previousOffset + previousSpace);
                if (newOffset > 100)
                {
                    newOffset -= 100;
                }

                attr["stroke-dashoffset"].Value = newOffset.ToString();
                root.InsertAfter(newnode, root.LastChild);

                previousOffset = newOffset;
                previousSpace = spaceDash;
            }

            root.RemoveChild(segmentNodeToCopy);
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.svg.DrawPictureAndFit(e);
        }
    }
}
