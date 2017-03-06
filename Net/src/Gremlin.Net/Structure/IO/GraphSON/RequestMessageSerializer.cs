using System.Collections.Generic;
using Gremlin.Net.Driver.Messages;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class RequestMessageSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            RequestMessage msg = objectData;
            return new Dictionary<string, dynamic>
            {
                {"requestId", writer.ToDict(msg.RequestId)},
                {"op", msg.Operation},
                {"processor", msg.Processor},
                {"args", writer.ToDict(msg.Arguments)}
            };
        }
    }
}