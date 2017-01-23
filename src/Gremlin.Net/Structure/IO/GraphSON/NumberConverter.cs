using System;
using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public abstract class NumberConverter : JsonConverter
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

            //var typedValue = new TypedValue(GraphSONTypeName, value);

            //var jObject = JObject.FromObject(typedValue);
            //jObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == HandledType;
        }
    }
}