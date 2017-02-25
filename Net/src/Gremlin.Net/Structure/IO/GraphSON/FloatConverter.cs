using System;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class FloatConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "Float";
        protected override Type HandledType => typeof(float);
    }
}