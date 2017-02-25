using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class Int32Converter : NumberConverter
    {
        protected override string GraphSONTypeName => "Int32";
        protected override Type HandledType => typeof(int);
    }
}