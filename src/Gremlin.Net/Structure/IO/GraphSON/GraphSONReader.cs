using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class GraphSONReader
    {
        private readonly Dictionary<string, IGraphSONDeserializer> _deserializerByGraphSONType = new Dictionary
            <string, IGraphSONDeserializer>
            {
                {"g:Traverser", new TraverserReader()},
                {"g:Int32", new Int32Converter()},
                {"g:Int64", new Int64Converter()},
                {"g:Float", new FloatConverter()},
                {"g:Double", new DoubleConverter()}
            };

        internal dynamic ToObject(JToken jToken)
        {
            if (jToken is JArray)
            {
                return jToken.Select(t => ToObject(t));
            }
            var graphSONType = (string)jToken["@type"];
            return _deserializerByGraphSONType[graphSONType].Objectify(jToken["@value"], this);
        }
    }
}