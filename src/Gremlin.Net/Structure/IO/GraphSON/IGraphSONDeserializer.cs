using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public interface IGraphSONDeserializer
    {
        dynamic Objectify(JToken graphsonObject, GraphSONReader reader);
    }
}