using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SVGChart.Nuget.Exceptions;

namespace SVGChart.Nuget.Charts
{
    internal class VerticalBarChart : BaseChart
    {
        public bool DisplayValues { get; set; } = true;
        public double BarWidth { get; set; }

        public VerticalBarChart()
        {
        }

        public override void UpdateSvg(XmlDocument document, IEnumerable items)
        {
            try
            {
                var elements = (IEnumerable<Tuple<int, string>>)items;

                double barPosition = 1;
                double textPosition = 6;
                double svgWidth = (elements.Count() * BarWidth);

                var svgElement = document.DocumentElement;
                var svgAttr = svgElement.Attributes;

                if (svgWidth <= 107)
                    svgWidth = 107;

                svgAttr["viewBox"].Value = $"-10 -5 {svgWidth} {svgWidth}";

                var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

                var barNodeToCopy = root.GetElementsByTagName("rect").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "rect-segment");

                var textNodeToCopy = root.GetElementsByTagName("text").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "text-segment");

                var lineToCopy = root.GetElementsByTagName("line").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "bottom-line");

                var lineAttr = lineToCopy.Attributes;
                lineAttr["x2"].Value = ((elements.Count() * BarWidth) + 5).ToString();

                foreach (var el in elements)
                {
                    if (el.Item1 < 0)
                        continue;

                    var newBarNode = barNodeToCopy.CloneNode(true);

                    var barAttr = newBarNode.Attributes;
                    barAttr["fill"].Value = el.Item2;
                    barAttr["height"].Value = el.Item1.ToString();
                    barAttr["x"].Value = barPosition.ToString();

                    var yPosition = 100 - el.Item1;

                    barAttr["y"].Value = yPosition.ToString();

                    root.InsertAfter(newBarNode, root.LastChild);
                    barPosition += BarWidth;


                    if (DisplayValues)
                    {
                        var newTextNode = textNodeToCopy.CloneNode(true);
                        newTextNode.InnerText = $"{el.Item1}%";
                        var textAttr = newTextNode.Attributes;
                        textAttr["x"].Value = textPosition.ToString();
                        root.InsertAfter(newTextNode, root.LastChild);
                        textPosition += BarWidth;
                    }
                }

                root.RemoveChild(barNodeToCopy);
                root.RemoveChild(textNodeToCopy);
            }
            catch (InvalidCastException ex)
            {
                throw new ItemsSourceException("ItemsSource for Vertical bar chart must to be a collection of values and colors IEnumerable<Tuple<int,string>>", ex);
            }
        }
    }
}
