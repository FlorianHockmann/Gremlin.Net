using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class TypedValue
    {
        [JsonProperty("@type")]
        public string GraphSONType { get; }
        [JsonProperty("@value")]
        public dynamic Value { get; }

        public TypedValue(string typeName, dynamic value, string namespacePrefix = "g")
        {
            GraphSONType = FormatTypeName(namespacePrefix, typeName);
            Value = value;
        }

        private string FormatTypeName(string namespacePrefix, string typeName)
        {
            return $"{namespacePrefix}:{typeName}";
        }
    }
}