using System.Collections.Generic;

namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal class AggregatorFactory
    {
        private readonly Dictionary<string, IAggregator> _aggregatorByAggregateToType =
            new Dictionary<string, IAggregator>
            {
                {Tokens.ValAggregateToMap, new DictionaryAggregator()},
                {Tokens.ValAggregateToBulkSet, new TraverserAggregator()}
            };

        public IAggregator GetAggregatorFor(string aggregateTo)
        {
            if (_aggregatorByAggregateToType.ContainsKey(aggregateTo))
                return _aggregatorByAggregateToType[aggregateTo];
            return new DefaultAggregator();
        }
    }
}