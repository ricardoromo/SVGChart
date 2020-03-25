﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SVGChart.Charts
{
    public class VBarChart : BaseChart
    {
        public bool DisplayValues { get; set; } = true;
        public double BarWidth { get; set; } = 10;

        public VBarChart(List<Tuple<int, string>> segments) : base(segments)
        {
            LoadSvg();
        }

        private void LoadSvg()
        {
            this.LoadAsXMLdocument<MainPage>("SVGChart.Resources.VBarChart.svg", document =>
            {
                var elements = Segments;
                if (elements == null)
                    return;
                UpdateSvg(document, elements);
            });
        }

        private void UpdateSvg(XmlDocument document, IEnumerable<Tuple<int, string>> elements)
        {
            double barPosition = 1;
            double textPosition = 6;
            double svgWidth = (elements.Count() * BarWidth) + 13;

            var svgElement = document.DocumentElement;
            var svgAttr = svgElement.Attributes;
            svgAttr["viewBox"].Value = $"-10 -5 {svgWidth} 107";

            var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

            var barNodeToCopy = root.GetElementsByTagName("rect").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "rect-segment");

            var textNodeToCopy = root.GetElementsByTagName("text").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "text-segment");

            var lineNodeToCopy = root.GetElementsByTagName("line").Cast<XmlElement>()
                                .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "bottom-line");

            var newLineNode = lineNodeToCopy.CloneNode(true);
            var lineAttr = newLineNode.Attributes;
            lineAttr["x2"].Value = ((elements.Count() * BarWidth) + 5).ToString();
           
            root.InsertAfter(newLineNode, root.LastChild);

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

            root.RemoveChild(lineNodeToCopy);
            root.RemoveChild(barNodeToCopy);
            root.RemoveChild(textNodeToCopy);
        }
    }
}
