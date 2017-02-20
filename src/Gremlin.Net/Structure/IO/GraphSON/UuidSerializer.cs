using System;
using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class UuidSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            Guid guid = objectData;
            return GraphSONUtil.ToTypedValue("UUID", guid);
        }
    }
}