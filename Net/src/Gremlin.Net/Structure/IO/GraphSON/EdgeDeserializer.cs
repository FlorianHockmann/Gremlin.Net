using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class EdgeDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var outVId = reader.ToObject(graphsonObject["outV"]);
            var outVLabel = (string) (graphsonObject["outVLabel"] ?? Vertex.DefaultLabel);
            var outV = new Vertex(outVId, outVLabel);
            var inVId = reader.ToObject(graphsonObject["inV"]);
            var inVLabel = (string) (graphsonObject["inVLabel"] ?? Vertex.DefaultLabel);
            var inV = new Vertex(inVId, inVLabel);
            var edgeId = reader.ToObject(graphsonObject["id"]);
            var edgeLabel = (string) graphsonObject["label"] ?? "edge";
            return new Edge(edgeId, outV, edgeLabel, inV);
        }
    }
}