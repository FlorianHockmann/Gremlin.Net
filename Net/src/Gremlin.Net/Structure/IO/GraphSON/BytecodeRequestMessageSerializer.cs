using System.Collections.Generic;
using Gremlin.Net.Driver.Messages.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class BytecodeRequestMessageSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            BytecodeRequestMessage msg = objectData;
            return new Dictionary<string, dynamic>
            {
                {"requestId", writer.ToDict(msg.RequestId)},
                {"op", msg.Operation },
                {"processor", msg.Processor },
                {
                    "args", new Dictionary<string, dynamic>
                    {
                        {"gremlin", writer.ToDict(msg.Arguments.Bytecode) },
                        {"aliases", writer.ToDict(msg.Arguments.Aliases) }
                    }
                }
            };
        }
    }
}