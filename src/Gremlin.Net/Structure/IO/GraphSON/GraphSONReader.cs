using System;
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
                {"g:Double", new DoubleConverter()},
                {"g:Vertex", new VertexDeserializer()},
                {"g:Edge", new EdgeDeserializer()}
            };

        public GraphSONReader()
        {
        }

        public GraphSONReader(Dictionary<string, IGraphSONDeserializer> deserializerByGraphSONType)
        {
            foreach (var deserializerAndGraphSONType in deserializerByGraphSONType)
            {
                _deserializerByGraphSONType.Add(deserializerAndGraphSONType.Key, deserializerAndGraphSONType.Value);
            }
        }

        public dynamic ToObject(JToken jToken)
        {
            if (jToken is JArray)
            {
                return jToken.Select(t => ToObject(t)).ToList();
            }
            if (jToken.HasValues)
            {
                if (HasTypeKey(jToken))
                {
                    return ReadTypedValue(jToken);
                }
                return ReadDictionary(jToken);
            }
            return ((JValue) jToken).Value;
        }

        private bool HasTypeKey(JToken jToken)
        {
            var graphSONType = (string)jToken[GraphSONTokens.TypeKey];
            return graphSONType != null;
        }

        private dynamic ReadTypedValue(JToken typedValue)
        {
            var graphSONType = (string)typedValue[GraphSONTokens.TypeKey];
            return _deserializerByGraphSONType[graphSONType].Objectify(typedValue[GraphSONTokens.ValueKey], this);
        }

        private dynamic ReadDictionary(JToken jtokenDict)
        {
            var dict = new Dictionary<string, dynamic>();
            foreach (var e in jtokenDict)
            {
                var property = e as JProperty;
                if (property == null)
                    throw new InvalidOperationException($"Cannot read graphson: {jtokenDict}");
                dict.Add(property.Name, ToObject(property.Value));
            }
            return dict;
        }
    }
}