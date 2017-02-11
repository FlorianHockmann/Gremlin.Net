using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class BytecodeSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic bytecode, GraphSONWriter writer)
        {
            var steps = bytecode.SourceInstructions;
            steps.AddRange(bytecode.StepInstructions);

            var valueDict = new Dictionary<string, List<List<dynamic>>> { { "step", new List<List<dynamic>>() } };

            foreach (var step in steps)
            {
                var stepTmp = new List<dynamic> { step.OperatorName };
                foreach(var arg in step.Arguments)
                    stepTmp.Add(writer.ToDict(arg));
                valueDict["step"].Add(stepTmp);
            }

            return GraphSONUtil.ToTypedValue(nameof(Bytecode), valueDict);
        }
    }
}