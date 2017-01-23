using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace Gremlin.Net.Process.Traversal
{
    //[JsonConverter(typeof(InstructionConverter))]
    public class Instruction
    {
        public string OperatorName { get; private set; }
        public dynamic[] Arguments { get; private set; }

        public Instruction(string operatorName, params dynamic[] arguments)
        {
            OperatorName = operatorName;
            Arguments = arguments;
        }
    }
}