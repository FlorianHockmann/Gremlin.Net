using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.UnitTest.Process
{
    public class TestTraversalStrategy : ITraversalStrategy
    {
        private readonly IEnumerable<Traverser> _traversers;

        public TestTraversalStrategy(IEnumerable<Traverser> traversersToAddOnApplication)
        {
            _traversers = traversersToAddOnApplication;
        }

        public void Apply(Traversal traversal)
        {
            traversal.Traversers = _traversers;
        }
    }
}