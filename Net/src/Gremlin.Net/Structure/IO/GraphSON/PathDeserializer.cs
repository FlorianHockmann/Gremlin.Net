using System.Linq;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class PathDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var labels =
                graphsonObject["labels"]
                    .Select(readObjLabels => readObjLabels.Select(l => (string) l).ToList())
                    .ToList();
            var objects = graphsonObject["objects"].Select(o => reader.ToObject(o)).ToList();
            return new Path(labels, objects);
        }
    }
}