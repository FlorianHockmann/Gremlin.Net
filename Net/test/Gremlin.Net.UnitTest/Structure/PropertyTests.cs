using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class PropertyTests
    {
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

        [Fact]
        public void Equals_EqualProperty_ReturnsTrue()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("age", 29, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_OtherIsNull_ReturnsFalse()
        {
            var property = new Property("age", 29, new Vertex(1));

            var areEqual = property.Equals(null);

            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_DifferentKey_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("aDifferentKey", 29, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("age", 12, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_DifferentElement_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("age", 29, new Vertex(1234));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void EqualsObject_EqualProperty_ReturnsTrue()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            object secondProperty = new Property("age", 29, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.True(areEqual);
        }

        [Fact]
        public void EqualsObject_DifferentKey_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            object secondProperty = new Property("aDifferentKey", 29, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void EqualsObject_DifferentValue_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            object secondProperty = new Property("age", 12, new Vertex(1));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void EqualsObject_DifferentElement_ReturnsFalse()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            object secondProperty = new Property("age", 29, new Vertex(1234));

            var areEqual = firstProperty.Equals(secondProperty);

            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_EqualProperty_EqualHashCodes()
        {
            var firstProperty = new Property("age", 29, new Vertex(1));
            var secondProperty = new Property("age", 29, new Vertex(1));

            var firstHashCode = firstProperty.GetHashCode();
            var secondHashCode = secondProperty.GetHashCode();

            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Fact]
        public void ToString_SimpleVertexProperty_CommonEdgeRepresentation()
        {
            var property = new Property("age", 29, new Vertex(1));

            var stringRepresentation = property.ToString();

            Assert.Equal("p[age->29]", stringRepresentation);
        }
    }
}