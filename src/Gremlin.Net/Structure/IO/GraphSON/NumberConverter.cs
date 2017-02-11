using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public abstract class NumberConverter : JsonConverter, IGraphSONDeserializer, IGraphSONSerializer
    {
        protected abstract string GraphSONTypeName { get; }
        protected abstract Type HandledType { get; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(GraphSONTokens.TypeKey);
            writer.WriteValue($"{GraphSONTokens.GremlinTypeNamespace}:{GraphSONTypeName}");
            writer.WritePropertyName(GraphSONTokens.ValueKey);
            writer.WriteValue(value);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == HandledType;
        }

        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            return graphsonObject.ToObject(HandledType);
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            return GraphSONUtil.ToTypedValue(GraphSONTypeName, objectData);
        }
    }
}