using System;
using System.Collections.Generic;
using System.Reflection;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class BytecodeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytecode = value as Bytecode;
            var steps = bytecode.SourceInstructions;
            steps.AddRange(bytecode.StepInstructions);

            var valueDict = new Dictionary<string, List<List<dynamic>>> {{"step", new List<List<dynamic>>()}};

            foreach (var step in steps)
            {
                var stepTmp = new List<dynamic> {step.OperatorName};
                stepTmp.AddRange(step.Arguments);
                valueDict["step"].Add(stepTmp);
            }

            var typedValue = new TypedValue(nameof(Bytecode), valueDict);
            //var jObject = JObject.FromObject(typedValue);
            //jObject.WriteTo(writer);
            serializer.Serialize(writer, typedValue);
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