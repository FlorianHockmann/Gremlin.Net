namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     Represents a <see cref="Bytecode" /> instruction by an operator name and its arguments.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Instruction" /> class.
        /// </summary>
        /// <param name="operatorName">The name of the operator.</param>
        /// <param name="arguments">The arguments.</param>
        public Instruction(string operatorName, params dynamic[] arguments)
        {
            OperatorName = operatorName;
            Arguments = arguments;
        }

        /// <summary>
        ///     Gets the name of the operator.
        /// </summary>
        public string OperatorName { get; }

        /// <summary>
        ///     Gets the arguments.
        /// </summary>
        public dynamic[] Arguments { get; }
    }
}