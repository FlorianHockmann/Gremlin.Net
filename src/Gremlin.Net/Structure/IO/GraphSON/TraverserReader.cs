using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class TraverserReader : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var bulkObj = reader.ToObject(graphsonObject["bulk"]);
            var valueObj = reader.ToObject(graphsonObject["value"]);
            return new Traverser(valueObj, bulkObj);
        }
    }
}