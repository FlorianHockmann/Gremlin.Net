using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class VertexSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            Vertex vertex = objectData;
            var vertexDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(vertex.Id)},
                {"label", writer.ToDict(vertex.Label)}
            };
            return GraphSONUtil.ToTypedValue(nameof(Vertex), vertexDict);
        }
    }
}