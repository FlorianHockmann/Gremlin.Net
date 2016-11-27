namespace Gremlin.Net.Process.Traversal
{
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