using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SkiaSharp.Extended.Svg;

namespace SVGChart.Charts
{
    public class BaseChart : SKSvg
    {
        public readonly List<Tuple<int, string>> Segments;
        public BaseChart(List<Tuple<int, string>> segments)
        {
            this.Segments = segments;
        }
    }
}
