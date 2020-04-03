using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SVGChart.Nuget.Exceptions;
using Xamarin.Forms;

namespace SVGChart.Nuget.Charts
{
    internal class LineChart : BaseChart
    {
        public Color LineColor { get; set; }
        public Color PointsColor { get; set; }
        public double PointsDistance { get; set; }
        public double PointSize { get; set; }
        public double LineWidth { get; set; }
        public bool ShowPoints { get; set; }

        public LineChart()
        {
        }

        public override void UpdateSvg(XmlDocument document, IEnumerable items)
        {
            try
            {
                var elements = (IEnumerable<int>)items;

                string linePoints = string.Empty;
                double pointPosition = 0;
                double svgWidth = (elements.Count() * PointsDistance) + 13;

                var svgElement = document.DocumentElement;
                var svgAttr = svgElement.Attributes;

                if (svgWidth <= 110)
                    svgWidth = 110;

                svgAttr["viewBox"].Value = $"-10 -5 {svgWidth} {svgWidth}";

                var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

                var polylineNodeToCopy = root.GetElementsByTagName("polyline").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "line-chart");

                var pointNodeToCopy = root.GetElementsByTagName("circle").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "point-chart");


                var lineToCopy = root.GetElementsByTagName("line").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "bottom-line");

                var lineAttr = lineToCopy.Attributes;
                lineAttr["x2"].Value = ((elements.Count() * PointsDistance)).ToString();


                foreach (var el in elements)
                {
                    if (el < 0)
                        continue;

                    var yPosition = 100 - el;

                    if (ShowPoints)
                    {
                        var newPointNode = pointNodeToCopy.CloneNode(true);

                        var pointAttr = newPointNode.Attributes;
                        pointAttr["cx"].Value = pointPosition.ToString();
                        pointAttr["fill"].Value = PointsColor.ToHex();
                        pointAttr["r"].Value = PointSize.ToString();
                        pointAttr["cy"].Value = yPosition.ToString();

                        root.InsertAfter(newPointNode, root.LastChild);
                    }

                    linePoints += $"{pointPosition},{yPosition - 1} ";
                    pointPosition += PointsDistance;
                }

                var barAttr = polylineNodeToCopy.Attributes;
                barAttr["points"].Value = linePoints;
                barAttr["stroke"].Value = LineColor.ToHex();
                barAttr["stroke-width"].Value = LineWidth.ToString();

                root.RemoveChild(pointNodeToCopy);
            }
            catch (InvalidCastException ex)
            {
                throw new ItemsSourceException("ItemsSource for Line chart must to be a collection of values IEnumerable<int>", ex);
            }
        }
    }
}
