using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class PropertyTests
    {
        [Fact]
        public void ToString_SimpleVertexProperty_CommonEdgeRepresentation()
        {
            var property = new Property("age", 29, new Vertex(1));

            var stringRepresentation = property.ToString();

            Assert.Equal("p[age->29]", stringRepresentation);
        }

        [Fact]
        public void Equals_EqualProperty_ReturnsTrue()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("age", 29, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.True(areEqual);
        }

        [Fact]
        public void Constructor_ValidArguments_InitializeProperties()
        {
            const string key = "age";
            const int value = 29;
            var element = new Vertex(1);

            var property = new Property(key, value, element);

            Assert.Equal(key, property.Key);
            Assert.Equal(value, property.Value);
            Assert.Equal(element, property.Element);
        }
    }
}