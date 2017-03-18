using System.Collections.Generic;
using Gremlin.Net.Process.Traversal.Strategy;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class TraversalStrategySerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            AbstractTraversalStrategy strategy = objectData;
            return GraphSONUtil.ToTypedValue(strategy.StrategyName, writer.ToDict(strategy.Configuration));
        }
    }
}