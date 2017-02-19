using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.UnitTest.Process.Traversal
{
    public class TestTraversalStrategy : ITraversalStrategy
    {
        private readonly IEnumerable<Traverser> _traversers;

        public TestTraversalStrategy(IEnumerable<Traverser> traversersToAddOnApplication)
        {
            _traversers = traversersToAddOnApplication;
        }

        public void Apply(Net.Process.Traversal.Traversal traversal)
        {
            traversal.Traversers = _traversers;
        }
    }
}