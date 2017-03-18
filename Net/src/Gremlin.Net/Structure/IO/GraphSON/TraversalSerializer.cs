using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class TraversalSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            var traversal = objectData as Traversal;
            var bytecode = traversal.Bytecode;
            return writer.ToDict(bytecode);
        }
    }
}