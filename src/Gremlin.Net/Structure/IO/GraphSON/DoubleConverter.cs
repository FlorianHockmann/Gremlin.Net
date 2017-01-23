using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class DoubleConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "Double";
        protected override Type HandledType => typeof(double);
    }
}