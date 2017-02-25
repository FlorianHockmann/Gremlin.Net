using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public interface IGraphSONSerializer
    {
        Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer);
    }
}