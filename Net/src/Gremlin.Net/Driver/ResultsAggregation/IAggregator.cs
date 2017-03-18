namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal interface IAggregator
    {
        void Add(object value);
        object GetAggregatedResult();
    }
}