using System;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using System.Reflection;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class InstructionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var instruction = value as Instruction;
            writer.WriteRaw($"[\"{instruction.OperatorName}\"]");
            //var bytecode = value as Bytecode;
            //writer.WriteStartObject();
            //writer.WritePropertyName(GraphSONTokens.ValueType);
            //serializer.Serialize(writer, $"{GraphSONTokens.GremlinTypeNamespace}:{nameof(Bytecode)}");
            //writer.WritePropertyName(GraphSONTokens.ValueProp);
            //writer.WriteStartObject();
            //writer.WritePropertyName("step");
            //var steps = bytecode.SourceInstructions;
            //steps.AddRange(bytecode.StepInstructions);
            //serializer.Serialize(writer, steps);
            //writer.WriteEndObject();
            //writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Instruction).IsAssignableFrom(objectType);
        }
    }
}