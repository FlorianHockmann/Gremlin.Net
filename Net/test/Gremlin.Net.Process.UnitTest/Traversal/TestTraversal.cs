using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Process.UnitTest.Traversal
{
    public class TestTraversal : Process.Traversal.Traversal
    {
        public TestTraversal(List<object> traverserObjs)
        {
            var traversers = new List<Traverser>(traverserObjs.Count);
            traverserObjs.ForEach(o => traversers.Add(new Traverser(o)));
            Traversers = traversers;
        }

        public TestTraversal(IReadOnlyList<object> traverserObjs, IReadOnlyList<long> traverserBulks)
        {
            var traversers = new List<Traverser>(traverserObjs.Count);
            traversers.AddRange(traverserObjs.Select((t, i) => new Traverser(t, traverserBulks[i])));
            Traversers = traversers;
        }

        public TestTraversal(IList<ITraversalStrategy> traversalStrategies)
        {
            TraversalStrategies = traversalStrategies;
        }
    }
}