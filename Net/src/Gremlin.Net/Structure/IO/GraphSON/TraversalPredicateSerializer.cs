using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class TraversalPredicateSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic predicate, GraphSONWriter writer)
        {
            TraversalPredicate p = predicate;
            var value = p.Other == null
                ? writer.ToDict(p.Value)
                : new List<dynamic> {writer.ToDict(p.Value), writer.ToDict(p.Other)};
            var dict = new Dictionary<string, dynamic>
            {
                {"predicate", p.OperatorName},
                {"value", value}
            };
            return GraphSONUtil.ToTypedValue("P", dict);
        }
    }
}