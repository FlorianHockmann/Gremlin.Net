using System.Collections.Generic;
using System.Linq;

namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    public class VertexProgramStrategy : AbstractTraversalStrategy
    {
        public VertexProgramStrategy(string graphComputer = null, int? workers = null, string persist = null,
            string result = null, Traversal vertices = null, Traversal edges = null,
            Dictionary<string, dynamic> configuration = null)
        {
            if (graphComputer != null)
                Configuration["graphComputer"] = graphComputer;
            if (workers != null)
                Configuration["workers"] = workers;
            if (persist != null)
                Configuration["persist"] = persist;
            if (result != null)
                Configuration["result"] = result;
            if (vertices != null)
                Configuration["vertices"] = vertices;
            if (edges != null)
                Configuration["edges"] = edges;
            configuration?.ToList().ForEach(x => Configuration[x.Key] = x.Value);
        }
    }
}