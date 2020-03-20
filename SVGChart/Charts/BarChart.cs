﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SVGChart.Charts
{
    public class BarChart : BaseChart
    {
        public bool DisplayValues { get; set; } = true;
        public double BarHeight { get; set; } = 10;

        public BarChart(List<Tuple<int, string>> segments) : base(segments)
        {
            LoadSvg();
        }

        private void LoadSvg()
        {
            this.LoadAsXMLdocument<MainPage>("SVGChart.Resources.BarChart.svg", document =>
            {
                var elements = Segments;
                if (elements == null)
                    return;
               UpdateSvg(document, elements);
            });
        }

        private void UpdateSvg(XmlDocument document, IEnumerable<Tuple<int, string>> elements)
        {
            double barPosition = 0;
            double textPosition = (BarHeight + 3) / 2;
            double countElementSize = (elements.Count() * BarHeight);
            double svgHeight = countElementSize + 13;
            string lineSize = (countElementSize + 1).ToString();

            var svgElement = document.DocumentElement;
            var svgAttr = svgElement.Attributes;
            svgAttr["viewBox"].Value = $"-2 0 110 {svgHeight}";

            var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

            var barNodeToCopy = root.GetElementsByTagName("rect").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "rect-segment");

            var textNodeToCopy = root.GetElementsByTagName("text").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "text-segment");

            var textValueNode = root.GetElementsByTagName("text").Cast<XmlElement>()
                                .Where(x => x.HasAttribute("class") && x.Attributes["class"].Value == "text-value");


            var lineNode = root.GetElementsByTagName("line").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "bottom-line");

            var lineAttr = lineNode.Attributes;
            lineAttr["y1"].Value = lineSize;
            lineAttr["y2"].Value = lineSize;

            var leftLineNode = root.GetElementsByTagName("line").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "left-line");

            var leftLineAttr = leftLineNode.Attributes;
            leftLineAttr["y2"].Value = lineSize;

            foreach (var node in textValueNode)
            {
                var textValueAttr = node.Attributes;
                textValueAttr["y"].Value = (countElementSize + 5).ToString();
            }

            foreach (var el in elements)
            {
                if (el.Item1 < 0)
                    continue;

                var newBarNode = barNodeToCopy.CloneNode(true);

                var barAttr = newBarNode.Attributes;
                barAttr["width"].Value = el.Item1.ToString();
                barAttr["fill"].Value = el.Item2;
                barAttr["y"].Value = barPosition.ToString();
                barAttr["height"].Value = BarHeight.ToString();

                root.InsertAfter(newBarNode, root.LastChild);
                barPosition += BarHeight;


                if (DisplayValues)
                {
                    var newTextNode = textNodeToCopy.CloneNode(true);
                    newTextNode.InnerText = $"{el.Item1}%";
                    var textAttr = newTextNode.Attributes;
                    textAttr["y"].Value = textPosition.ToString();
                    root.InsertAfter(newTextNode, root.LastChild);
                    textPosition += BarHeight;
                }
            }

            root.RemoveChild(barNodeToCopy);
            root.RemoveChild(textNodeToCopy);
        }
    }
}
