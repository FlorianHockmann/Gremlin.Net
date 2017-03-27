using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class VertexTests
    {
        [Fact]
        public void ShouldReturnCommonStringRepresentationForToString()
        {
            var vertex = new Vertex(12345);

            var vertexString = vertex.ToString();

            Assert.Equal("v[12345]", vertexString);
        }

        [Fact]
        public void ShouldReturnTrueForEqualsOfTwoEqualVertices()
        {
            var firstVertex = new Vertex(1);
            var secondVertex = new Vertex(1);

            var areEqual = firstVertex.Equals(secondVertex);

            Assert.True(areEqual);
        }

        [Fact]
        public void ShouldReturnFalseForEqualsWhereOtherIsNull()
        {
            var vertex = new Vertex(1);

            var areEqual = vertex.Equals(null);

            Assert.False(areEqual);
        }

        [Fact]
        public void ShouldReturnSpecifiedLabelForLabelProperty()
        {
            const string specifiedLabel = "person";

            var vertex = new Vertex(1, specifiedLabel);

            Assert.Equal(specifiedLabel, vertex.Label);
        }

        [Fact]
        public void ShouldReturnDefaultLabelForLabelPropertyWhenNoLabelSpecified()
        {
            var vertex = new Vertex(1);

            Assert.Equal("vertex", vertex.Label);
        }
    }
}