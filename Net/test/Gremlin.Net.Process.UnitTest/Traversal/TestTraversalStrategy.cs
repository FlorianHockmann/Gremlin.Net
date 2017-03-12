using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Process.UnitTest.Traversal
{
    public class TestTraversalStrategy : ITraversalStrategy
    {
        private readonly IEnumerable<Traverser> _traversers;

        public TestTraversalStrategy(IEnumerable<Traverser> traversersToAddOnApplication)
        {
            _traversers = traversersToAddOnApplication;
        }

        public void Apply(Process.Traversal.Traversal traversal)
        {
            traversal.Traversers = _traversers;
        }

        public Task ApplyAsync(Process.Traversal.Traversal traversal)
        {
            traversal.Traversers = _traversers;
            return Task.CompletedTask;
        }
    }
}