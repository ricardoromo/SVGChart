using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SkiaSharp.Extended.Svg;
using Xamarin.Forms;

namespace SVGChart.Charts
{
    public class LineChart : SKSvg
    {
        private Color strokeColor { get; set; } = Color.DodgerBlue;
        public int PointDistance { get; set; } = 12;

        public readonly List<int> Segments;
        public LineChart(List<int> segments, Color strokeColor) 
        {
            this.strokeColor = strokeColor;
            Segments = segments;
            LoadSvg();
        }

        private void LoadSvg()
        {
            this.LoadAsXMLdocument<MainPage>("SVGChart.Resources.LinesChart.svg", document =>
            {
                var elements = Segments;
                if (elements == null)
                    return;
                UpdateSvg(document, Segments);
            });
        }

        private void UpdateSvg(XmlDocument document, IEnumerable<int> elements)
        {
            string linePoints = string.Empty;
            double pointPosition = 0;
            double svgWidth = (elements.Count() * PointDistance) + 13;

            var svgElement = document.DocumentElement;
            var svgAttr = svgElement.Attributes;
            svgAttr["viewBox"].Value = $"-10 -5 {svgWidth} 105";

            var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

            var polylineNodeToCopy = root.GetElementsByTagName("polyline").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "line-chart");

            var pointNodeToCopy = root.GetElementsByTagName("circle").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "point-chart");

            foreach (var el in elements)
            {
                if (el < 0)
                    continue;

                var newPointNode = pointNodeToCopy.CloneNode(true);

                var pointAttr = newPointNode.Attributes;
                pointAttr["cx"].Value = pointPosition.ToString();
                

                var yPosition = 100 - el;
                pointAttr["cy"].Value = yPosition.ToString();

                linePoints += $"{pointPosition},{yPosition - 1} ";

                root.InsertAfter(newPointNode, root.LastChild);

                pointPosition += PointDistance;
            }


            var barAttr = polylineNodeToCopy.Attributes;
            barAttr["points"].Value = linePoints;
            barAttr["stroke"].Value = strokeColor.ToHex();

            root.RemoveChild(pointNodeToCopy);
        }
    }
}
