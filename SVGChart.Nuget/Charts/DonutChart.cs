using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SVGChart.Nuget.Exceptions;
using Xamarin.Forms;

namespace SVGChart.Nuget.Charts
{
    internal class DonutChart : BaseChart
    {
        public Color RingColor { get; set; }
        public Color FillColor { get; set; }
        public Color TitleColor { get; set; }
        public string CharTitle { get; set; }
        public int StrokeWidth { get; set; }

        public DonutChart()
        {
        }

        public override void UpdateSvg(XmlDocument document, IEnumerable items)
        {
            try
            {
                var elements = (IEnumerable<Tuple<int, string>>)items;

                var offset = 25;
                var previousOffset = -1;
                var previousSpace = -1;

                var root = document.DocumentElement.GetElementsByTagName("g").Cast<XmlElement>().LastOrDefault();

                var donutNode = root.GetElementsByTagName("circle").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-hole");

                var donutAttr = donutNode.Attributes;
                donutAttr["fill"].Value = FillColor.ToHex();

                var ringNode = root.GetElementsByTagName("circle").Cast<XmlElement>()
                                    .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-ring");

                var ringAttr = ringNode.Attributes;
                ringAttr["stroke-width"].Value = StrokeWidth.ToString();
                ringAttr["stroke"].Value = RingColor.ToHex();

                var segmentNodeToCopy = root.GetElementsByTagName("circle").Cast<XmlElement>()
                               .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "donut-segment");


                var centerTextNode = root.GetElementsByTagName("text").Cast<XmlElement>()
                  .FirstOrDefault(x => x.HasAttribute("class") && x.Attributes["class"].Value == "center-text");

                centerTextNode.InnerText = CharTitle;

                var textAttr = centerTextNode.Attributes;
                textAttr["fill"].Value = TitleColor.ToHex();

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
            }
            catch (InvalidCastException ex) {
                throw new ItemsSourceException("ItemsSource for Donut chart must to be a collection of values and colors IEnumerable<Tuple<int,string>>", ex);
            }
        }
    }
}
