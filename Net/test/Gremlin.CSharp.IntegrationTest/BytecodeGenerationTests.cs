using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class BytecodeGenerationTests
    {
        [Fact]
        public void g_V_OutXcreatedX()
        {
            var g = new Graph().Traversal();

            var bytecode = g.V().Out("created").Bytecode;

            Assert.Equal(0, bytecode.SourceInstructions.Count);
            Assert.Equal(2, bytecode.StepInstructions.Count);
            Assert.Equal("V", bytecode.StepInstructions[0].OperatorName);
            Assert.Equal("out", bytecode.StepInstructions[1].OperatorName);
            Assert.Equal("created", bytecode.StepInstructions[1].Arguments[0]);
            Assert.Equal(1, bytecode.StepInstructions[1].Arguments.Length);
        }

        [Fact]
        public void g_WithSackX1X_E_GroupCount_ByXweightX()
        {
            var g = new Graph().Traversal();

            var bytecode = g.WithSack(1).E().GroupCount().By("weight").Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal("withSack", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(1, bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal(3, bytecode.StepInstructions.Count);
            Assert.Equal("E", bytecode.StepInstructions[0].OperatorName);
            Assert.Equal("groupCount", bytecode.StepInstructions[1].OperatorName);
            Assert.Equal("by", bytecode.StepInstructions[2].OperatorName);
            Assert.Equal("weight", bytecode.StepInstructions[2].Arguments[0]);
            Assert.Equal(0, bytecode.StepInstructions[0].Arguments.Length);
            Assert.Equal(0, bytecode.StepInstructions[1].Arguments.Length);
            Assert.Equal(1, bytecode.StepInstructions[2].Arguments.Length);
        }

        [Fact]
        public void AnonymousTraversal_Start_EmptyBytecode()
        {
            var bytecode = __.Start().Bytecode;

            Assert.Equal(0, bytecode.SourceInstructions.Count);
            Assert.Equal(0, bytecode.StepInstructions.Count);
        }
    }
}