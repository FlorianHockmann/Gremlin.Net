using System;
using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class DateSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            DateTime dateTime = objectData;
            return GraphSONUtil.ToTypedValue("Date", ToJavaTimestamp(dateTime));
        }

        private long ToJavaTimestamp(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dateTime - epoch).TotalMilliseconds);
        }
    }
}