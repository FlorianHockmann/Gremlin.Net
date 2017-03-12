using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
using Gremlin.Net.Structure.IO.GraphSON;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    public class StrategyWriterTests
    {
        private GraphSONWriter CreateGraphSONWriter()
        {
            return new GraphSONWriter();
        }

        [Fact]
        public void WriteObject_SubgraphStrategyWithoutValues_ExpectedGraphSON()
        {
            var subgraphStrategy = new SubgraphStrategy();
            var writer = CreateGraphSONWriter();

            var graphSon = writer.WriteObject(subgraphStrategy);

            const string expected = "{\"@type\":\"g:SubgraphStrategy\",\"@value\":{}}";
            Assert.Equal(expected, graphSon);
        }

        [Fact]
        public void WriteObject_SubgraphStrategyWithVertexCriterion_ExpectedGraphSON()
        {
            var vertexCriterionBytecode = new Bytecode();
            vertexCriterionBytecode.AddStep("has", "name", "marko");
            var vertexCriterion = new TestTraversal(vertexCriterionBytecode);
            var subgraphStrategy = new SubgraphStrategy(vertexCriterion);
            var writer = CreateGraphSONWriter();

            var graphSon = writer.WriteObject(subgraphStrategy);

            const string expected =
                "{\"@type\":\"g:SubgraphStrategy\",\"@value\":{\"vertices\":{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"has\",\"name\",\"marko\"]]}}}}";
            Assert.Equal(expected, graphSon);
        }
    }
}