using System;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    internal class TestUtils
    {
        public static DateTime FromJavaTime(long javaTimestamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(javaTimestamp);
        }
    }
}