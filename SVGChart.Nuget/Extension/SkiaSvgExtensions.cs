using System;
using System.IO;
using System.Xml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;
namespace SVGChart.Nuget.Extension
{
    public static class SkiaSvgExtensions
    {
        public static void LoadAsXMLdocument<T>(this SKSvg svg, string svgNamePath, Action<XmlDocument> actionXmlDoc = null)
        {
            var assembly = typeof(T).Assembly;

            using (var stream = assembly.GetManifestResourceStream(svgNamePath))
            {
                var document = new XmlDocument();
                using (var reader = XmlReader.Create(stream))
                {
                    document.Load(reader);
                }

                actionXmlDoc?.Invoke(document);
                svg.Load(document.XmlDocumentToStream());
            }
        }

        public static void DrawPictureAndFit(this SKSvg svg, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            var width = e.Info.Width;
            var height = e.Info.Height;

            canvas.Clear(SKColors.Transparent);

            if (svg == null || svg.Picture == null)
                return;

            float xRatio = width / svg.Picture.CullRect.Width;
            float yRatio = height  / svg.Picture.CullRect.Height;

            float ratio = Math.Min(xRatio, yRatio);

            var matrix = SKMatrix.MakeScale(ratio, ratio);
            canvas.DrawPicture(svg.Picture, ref matrix);
        }

        private static Stream XmlDocumentToStream(this XmlDocument xml)
        {
            var xmlStream = new MemoryStream();
            xml.Save(xmlStream);

            xmlStream.Flush();
            xmlStream.Position = 0;

            return xmlStream;
        }
    }
}
