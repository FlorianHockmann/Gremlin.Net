using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class BytecodeSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic bytecodeObj, GraphSONWriter writer)
        {
            Bytecode bytecode = bytecodeObj;

            var valueDict = new Dictionary<string, List<List<dynamic>>>();
            if (bytecode.SourceInstructions.Count > 0)
            {
                valueDict["source"] = DictifyInstructions(bytecode.SourceInstructions, writer);
            }
            if (bytecode.StepInstructions.Count > 0)
            {
                valueDict["step"] = DictifyInstructions(bytecode.StepInstructions, writer);
            }

            return GraphSONUtil.ToTypedValue(nameof(Bytecode), valueDict);
        }

        private List<List<dynamic>> DictifyInstructions(IEnumerable<Instruction> instructions, GraphSONWriter writer)
        {
            var result = new List<List<dynamic>>();
            foreach (var instruction in instructions)
            {
                var dictifiedInstruction = new List<dynamic> { instruction.OperatorName };
                dictifiedInstruction.AddRange(instruction.Arguments.Select(arg => writer.ToDict(arg)));
                result.Add(dictifiedInstruction);
            }
            return result;
        }
    }
}