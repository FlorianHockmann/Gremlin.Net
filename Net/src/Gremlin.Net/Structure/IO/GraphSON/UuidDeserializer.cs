using System;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class UuidDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            return graphsonObject.ToObject<Guid>();
        }
    }
}