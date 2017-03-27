using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class EdgeTests
    {
        [Fact]
        public void ShouldAssignPropertiesCorrectly()
        {
            const int id = 2;
            var outV = new Vertex(1);
            const string edgeLabel = "said";
            var inV = new Vertex("hello", "phrase");

            var edge = new Edge(id, outV, edgeLabel, inV);

            Assert.Equal(outV, edge.OutV);
            Assert.Equal(inV, edge.InV);
            Assert.Equal(edgeLabel, edge.Label);
            Assert.NotEqual(edge.InV, edge.OutV);
        }

        [Fact]
        public void ShouldReturnCommonStringRepresentationForToString()
        {
            var edge = new Edge(2, new Vertex(1), "said", new Vertex("hello", "phrase"));

            var edgeStr = edge.ToString();

            Assert.Equal("e[2][1-said->hello]", edgeStr);
        }
    }
}