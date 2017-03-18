using System;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class DateDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var javaTimestamp = graphsonObject.ToObject<long>();
            return FromJavaTime(javaTimestamp);
        }

        private DateTime FromJavaTime(long javaTimestamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(javaTimestamp);
        }
    }
}