using System;
using System.Reflection;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class BytecodeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytecode = value as Bytecode;
            writer.WriteStartObject();
            writer.WritePropertyName(GraphSONTokens.ValueType);
            serializer.Serialize(writer, $"{GraphSONTokens.GremlinTypeNamespace}:{nameof(Bytecode)}");
            writer.WritePropertyName(GraphSONTokens.ValueProp);
            writer.WriteStartObject();
            writer.WritePropertyName("step");
            var steps = bytecode.SourceInstructions;
            steps.AddRange(bytecode.StepInstructions);

            var serializedSteps = "[";
            for (var i = 0; i < steps.Count - 1; i++)
                serializedSteps += JsonConvert.SerializeObject(steps[i]) + ",";
            serializedSteps += JsonConvert.SerializeObject(steps[steps.Count - 1]);
            serializedSteps += "]";
            writer.WriteRawValue(serializedSteps);

            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Bytecode).IsAssignableFrom(objectType);
        }
    }
}