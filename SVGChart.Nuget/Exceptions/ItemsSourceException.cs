using System;
namespace SVGChart.Nuget.Exceptions
{
    public class ItemsSourceException : Exception
    {
        public ItemsSourceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
