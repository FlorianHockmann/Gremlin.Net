using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class VertexDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var id = reader.ToObject(graphsonObject["id"]);
            var label = (string) graphsonObject["label"] ?? Vertex.DefaultLabel;
            return new Vertex(id, label);
        }
    }
}