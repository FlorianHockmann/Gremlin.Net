using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    /// <summary>
    ///     Provides helper methods for GraphSON serialization.
    /// </summary>
    public static class GraphSONUtil
    {
        /// <summary>
        ///     Transforms a value intos its GraphSON representation including type information.
        /// </summary>
        /// <param name="typename">The name of the type.</param>
        /// <param name="value">The value to transform.</param>
        /// <param name="prefix">A namespace prefix for the typename.</param>
        /// <returns>The GraphSON representation including type information.</returns>
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