using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class VertexPropertyDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var id = reader.ToObject(graphsonObject["id"]);
            var label = (string) graphsonObject["label"];
            var value = reader.ToObject(graphsonObject["value"]);
            var vertex = graphsonObject["vertex"] != null ? new Vertex(reader.ToObject(graphsonObject["vertex"])) : null;
            return new VertexProperty(id, label, value, vertex);
        }
    }
}