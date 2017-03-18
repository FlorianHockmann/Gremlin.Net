using System.Collections.Generic;

namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal class DefaultAggregator : IAggregator
    {
        private readonly List<dynamic> _result = new List<dynamic>();

        public void Add(object value)
        {
            _result.Add(value);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }
}