using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class BytecodeSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic bytecodeObj, GraphSONWriter writer)
        {
            Bytecode bytecode = bytecodeObj;

            var valueDict = new Dictionary<string, IEnumerable<IEnumerable<dynamic>>>();
            if (bytecode.SourceInstructions.Count > 0)
                valueDict["source"] = DictifyInstructions(bytecode.SourceInstructions, writer);
            if (bytecode.StepInstructions.Count > 0)
                valueDict["step"] = DictifyInstructions(bytecode.StepInstructions, writer);

            return GraphSONUtil.ToTypedValue(nameof(Bytecode), valueDict);
        }

        private IEnumerable<IEnumerable<dynamic>> DictifyInstructions(IEnumerable<Instruction> instructions,
            GraphSONWriter writer)
        {
            return instructions.Select(instruction => DictifyInstruction(instruction, writer));
        }

        private IEnumerable<dynamic> DictifyInstruction(Instruction instruction, GraphSONWriter writer)
        {
            var result = new List<dynamic> {instruction.OperatorName};
            result.AddRange(instruction.Arguments.Select(arg => writer.ToDict(arg)));
            return result;
        }
    }
}