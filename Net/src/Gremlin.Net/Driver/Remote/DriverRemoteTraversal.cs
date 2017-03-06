using System;
using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    public class DriverRemoteTraversal : Traversal
    {
        public DriverRemoteTraversal(IGremlinClient gremlinClient, Guid requestId,
            IEnumerable<Traverser> traversers)
        {
            Traversers = traversers;
            SideEffects = new DriverRemoteTraversalSideEffects(gremlinClient, requestId);
        }
    }
}