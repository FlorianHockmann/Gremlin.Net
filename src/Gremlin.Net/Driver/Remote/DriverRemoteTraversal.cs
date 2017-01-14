using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    public class DriverRemoteTraversal : Traversal
    {
        private readonly IGremlinClient _gremlinClient;

        public DriverRemoteTraversal(IGremlinClient gremlinClient, IEnumerable<Traverser> traversers)
        {
            _gremlinClient = gremlinClient;
            Traversers = traversers;

            SideEffects = null; // TODO: Create SideEffects
        }
    }
}