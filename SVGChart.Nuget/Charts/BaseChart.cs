using System.Collections;
using System.Xml;
using SkiaSharp.Extended.Svg;
using SVGChart.Nuget.Extension;
using SVGChart.Nuget.Utils;

namespace SVGChart.Nuget.Charts
{
    public class BaseChart : SKSvg
    {
        public IEnumerable Segments { get; set; }
        public BaseChart()
        {

        }

        public void LoadSvg(ChartType chartType)
        {
            this.LoadAsXMLdocument<BaseChart>($"SVGChart.Nuget.Resources.{chartType}.svg", document =>
            {
                var elements = Segments;
                if (elements == null)
                    return;
                UpdateSvg(document, elements);
            });
        }

        public virtual void UpdateSvg(XmlDocument document, IEnumerable elements)
        {

        }
    }
}
