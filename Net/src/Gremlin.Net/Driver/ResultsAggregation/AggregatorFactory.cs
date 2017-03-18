namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal class AggregatorFactory
    {
        public IAggregator GetAggregatorFor(string aggregateTo)
        {
            if (aggregateTo == "map")
                return new DictionaryAggregator();
            if (aggregateTo == "bulkset")
                return new TraverserAggregator();
            return new DefaultAggregator();
        }
    }
}