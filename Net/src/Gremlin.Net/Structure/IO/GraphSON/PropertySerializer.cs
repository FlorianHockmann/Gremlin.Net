using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class PropertySerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            Property property = objectData;
            var elementDict = CreateElementDict(property.Element, writer);
            var valueDict = new Dictionary<string, dynamic>
            {
                {"key", writer.ToDict(property.Key)},
                {"value", writer.ToDict(property.Value)},
                {"element", elementDict}
            };
            return GraphSONUtil.ToTypedValue(nameof(Property), valueDict);
        }

        private dynamic CreateElementDict(Element element, GraphSONWriter writer)
        {
            if (element == null)
                return null;
            var serializedElement = writer.ToDict(element);
            Dictionary<string, dynamic> elementDict = serializedElement;
            if (elementDict.ContainsKey(GraphSONTokens.ValueKey))
            {
                var elementValueSerialized = elementDict[GraphSONTokens.ValueKey];
                Dictionary<string, dynamic> elementValueDict = elementValueSerialized;
                if (elementValueDict != null)
                {
                    elementValueDict.Remove("outVLabel");
                    elementValueDict.Remove("inVLabel");
                    elementValueDict.Remove("properties");
                    elementValueDict.Remove("value");
                }
            }
            return serializedElement;
        }
    }
}