using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public static class GraphSONUtil
    {
        public static Dictionary<string, dynamic> ToTypedValue(string typename, dynamic value, string prefix = "g")
        {
            var typedValue = new Dictionary<string, dynamic>
            {
                {GraphSONTokens.TypeKey, FormatTypeName(prefix, typename)}
            };
            if (value != null)
                typedValue[GraphSONTokens.ValueKey] = value;
            return typedValue;
        }

        private static string FormatTypeName(string namespacePrefix, string typeName)
        {
            return $"{namespacePrefix}:{typeName}";
        }
    }
}