using System;
using System.Reflection;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class TraverserConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new Traverser(1);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Traverser).IsAssignableFrom(objectType);
        }
    }
}