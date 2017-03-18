using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class VertexPropertySerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            VertexProperty vertexProperty = objectData;
            var valueDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(vertexProperty.Id)},
                {"label", writer.ToDict(vertexProperty.Label)},
                {"value", writer.ToDict(vertexProperty.Value)},
                {"vertex", writer.ToDict(vertexProperty.Vertex.Id)}
            };
            return GraphSONUtil.ToTypedValue(nameof(VertexProperty), valueDict);
        }
    }
}