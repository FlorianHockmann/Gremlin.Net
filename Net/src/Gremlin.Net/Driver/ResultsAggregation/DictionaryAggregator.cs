using System.Collections.Generic;
using System.Linq;

namespace Gremlin.Net.Driver.ResultsAggregation
{
    internal class DictionaryAggregator : IAggregator
    {
        private readonly Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();

        public void Add(object value)
        {
            var newEntry = ((Dictionary<string, dynamic>) value).First();
            _result.Add(newEntry.Key, newEntry.Value);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }
}