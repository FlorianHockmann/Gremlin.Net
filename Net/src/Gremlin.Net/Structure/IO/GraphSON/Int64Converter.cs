using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class Int64Converter : NumberConverter
    {
        protected override string GraphSONTypeName => "Int64";
        protected override Type HandledType => typeof(long);
    }
}