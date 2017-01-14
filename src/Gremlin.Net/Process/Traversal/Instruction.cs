using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace Gremlin.Net.Process.Traversal
{
    [JsonConverter(typeof(InstructionConverter))]
    public class Instruction
    {
        public string OperatorName { get; private set; }
        public object[] Arguments { get; private set; }

        public Instruction(string operatorName, params object[] arguments)
        {
            OperatorName = operatorName;
            Arguments = arguments;
        }
    }
}