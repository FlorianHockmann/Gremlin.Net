using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class EdgeSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            Edge edge = objectData;
            var edgeDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(edge.Id)},
                {"outV", writer.ToDict(edge.OutV.Id)},
                {"outVLabel", writer.ToDict(edge.OutV.Label)},
                {"label", writer.ToDict(edge.Label)},
                {"inV", writer.ToDict(edge.InV.Id)},
                {"inVLabel", writer.ToDict(edge.InV.Label)}
            };
            return GraphSONUtil.ToTypedValue(nameof(Edge), edgeDict);
        }
    }
}