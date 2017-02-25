namespace Gremlin.Net.Driver.Messages.Traversal
{
    public class BytecodeRequestMessage : TraversalRequestMessage<BytecodeArguments>
    {
        public override string Operation => "bytecode";
    }
}