using System;
using System.IO;
using System.Xml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace SVGChart
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

            if (svg == null)
                return;

            float canvasMin = Math.Min(width, height);
            var svgMax = Math.Max(svg.Picture.CullRect.Width, svg.Picture.CullRect.Height);
            var scale = canvasMin / svgMax;
            var matrix = SKMatrix.MakeScale(scale, scale);

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
