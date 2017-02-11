using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class EnumSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            var enumName = objectData.GetType().Name;
            var enumValue = objectData.ToString();
            return GraphSONUtil.ToTypedValue(enumName, enumValue);
        }
    }
}