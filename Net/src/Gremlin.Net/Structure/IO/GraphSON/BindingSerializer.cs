using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class BindingSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            var binding = (Binding) objectData;
            var valueDict = new Dictionary<string, object>
            {
                {"value", writer.ToDict(binding.Value)},
                {"key", binding.Key}
            };
            return GraphSONUtil.ToTypedValue(nameof(Binding), valueDict);
        }
    }
}