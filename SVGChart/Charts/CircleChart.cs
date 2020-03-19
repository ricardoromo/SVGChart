using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SVGChart.Charts
{
    public class CircleChart : BaseChart
    {
        public string RingColor { get; set; } = "#e6e6e6";
        public float StrokeWidth { get; set; } = 5;

        public CircleChart(List<Tuple<int, string>> segments) : base(segments)
        {
            LoadSvg();
        }

        private void LoadSvg()
        {
            this.LoadAsXMLdocument<MainPage>("SVGChart.Resources.CircleChart.svg", document =>
            {
                var elements = Segments;
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
            ringAttr["stroke-width"].Value = StrokeWidth.ToString();
            ringAttr["stroke"].Value = RingColor;

            var segmentNodeToCopy = root.GetElementsByTagName("circle").Cast<XmlElement>()
                           .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-segment");

            foreach (var el in elements)
            {
                if (el.Item1 < 0)
                    continue;

                var newnode = segmentNodeToCopy.CloneNode(true);
                var attr = newnode.Attributes;
                attr["stroke-width"].Value = StrokeWidth.ToString();
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

            var centerTextCopy = root.GetElementsByTagName("text").Cast<XmlElement>()
                          .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "center-text");

            var textNode = centerTextCopy.CloneNode(true);
            var textAttr = textNode.Attributes;
            textAttr["fill"].Value = "#cccccc";
            textNode.InnerText = "10%";
            root.InsertAfter(textNode, root.LastChild);
            root.RemoveChild(centerTextCopy);
        }
    }
}
