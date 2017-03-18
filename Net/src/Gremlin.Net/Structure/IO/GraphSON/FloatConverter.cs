using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class FloatConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "Float";
        protected override Type HandledType => typeof(float);
    }
}