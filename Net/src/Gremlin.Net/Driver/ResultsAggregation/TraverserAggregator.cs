using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal class TraverserAggregator : IAggregator
    {
        private readonly Dictionary<object, long> _result = new Dictionary<object, long>();

        public void Add(object value)
        {
            var traverser = (Traverser) value;
            _result.Add(traverser.Object, traverser.Bulk);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }
}