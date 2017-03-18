using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class PropertyDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var key = (string) graphsonObject["key"];
            var value = reader.ToObject(graphsonObject["value"]);
            var element = graphsonObject["element"] != null ? reader.ToObject(graphsonObject["element"]) : null;
            return new Property(key, value, element);
        }
    }
}