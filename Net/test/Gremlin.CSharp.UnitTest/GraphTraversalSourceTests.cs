using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.UnitTest
{
    public class GraphTraversalSourceTests
    {
        [Fact]
        public void ShouldBeIndependentFromReturnedGraphTraversalModififyingBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            g.V().Has("someKey", "someValue").Drop();

            Assert.Equal(0, g.Bytecode.StepInstructions.Count);
            Assert.Equal(0, g.Bytecode.SourceInstructions.Count);
        }

        [Fact]
        public void ShouldBeIndependentFromReturnedGraphTraversalSourceModififyingBytecode()
        {
            var graph = new Graph();
            var g1 = graph.Traversal();

            var g2 = g1.WithSideEffect("someSideEffectKey", "someSideEffectValue");

            Assert.Equal(0, g1.Bytecode.SourceInstructions.Count);
            Assert.Equal(0, g1.Bytecode.StepInstructions.Count);
            Assert.Equal(1, g2.Bytecode.SourceInstructions.Count);
        }

        [Fact]
        public void ShouldBeIndependentFromReturnedGraphTraversalSourceModififyingTraversalStrategies()
        {
            var graph = new Graph();
            var gLocal = graph.Traversal();

            var gRemote = gLocal.WithRemote(null);

            Assert.Equal(0, gLocal.TraversalStrategies.Count);
            Assert.Equal(1, gRemote.TraversalStrategies.Count);
        }
    }
}