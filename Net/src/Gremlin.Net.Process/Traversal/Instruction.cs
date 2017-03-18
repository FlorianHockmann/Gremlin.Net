namespace Gremlin.Net.Process.Traversal
{
    public class Instruction
    {
        public Instruction(string operatorName, params dynamic[] arguments)
        {
            OperatorName = operatorName;
            Arguments = arguments;
        }

        public string OperatorName { get; }
        public dynamic[] Arguments { get; }
    }
}