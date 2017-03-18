using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class DoubleConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "Double";
        protected override Type HandledType => typeof(double);
    }
}