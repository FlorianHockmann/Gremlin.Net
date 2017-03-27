using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class VertexPropertyTests
    {
        [Fact]
        public void ShouldAssignPropertiesCorrectly()
        {
            const long id = 24;
            const string label = "name";
            const string value = "marko";
            var vertex = new Vertex(1);

            var vertexProperty = new VertexProperty(id, label, value, vertex);

            Assert.Equal(label, vertexProperty.Label);
            Assert.Equal(label, vertexProperty.Key);
            Assert.Equal(value, vertexProperty.Value);
            Assert.Equal(id, vertexProperty.Id);
            Assert.Equal(vertex, vertexProperty.Vertex);
        }

        [Fact]
        public void ShouldReturnTrueForEqualsOfTwoEqualVertexProperties()
        {
            var firstVertexProperty = new VertexProperty((long) 24, "name", "marko", new Vertex(1));
            var secondVertexProperty = new VertexProperty((long) 24, "name", "marko", new Vertex(1));

            var areEqual = firstVertexProperty.Equals(secondVertexProperty);

            Assert.True(areEqual);
        }

        [Fact]
        public void ShouldReturnCommonStringRepresentationForToString()
        {
            var vertexProperty = new VertexProperty((long) 24, "name", "marko", new Vertex(1));

            var stringRepresentation = vertexProperty.ToString();

            Assert.Equal("vp[name->marko]", stringRepresentation);
        }
    }
}