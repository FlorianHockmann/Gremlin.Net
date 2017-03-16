using Gremlin.Net.Process.Traversal;
using Xunit;

namespace Gremlin.Net.Process.UnitTest.Traversal
{
    public class TraverserTests
    {
        [Fact]
        public void Equals_OtherIsNull_ReturnFalse()
        {
            var traverser = new Traverser("anObject");

            var areEqual = traverser.Equals(null);

            Assert.False(areEqual);
        }

        [Fact]
        public void Equals_SameObjectDifferentBulk_ReturnTrue()
        {
            var firstTraverser = new Traverser("anObject", 1234);
            var secondTraverser = new Traverser("anObject", 9876);

            var areEqual = firstTraverser.Equals(secondTraverser);

            Assert.True(areEqual);
        }

        [Fact]
        public void EqualsObject_SameObjectDifferentBulk_ReturnTrue()
        {
            var firstTraverser = new Traverser("anObject", 1234);
            object secondTraverser = new Traverser("anObject", 9876);

            var areEqual = firstTraverser.Equals(secondTraverser);

            Assert.True(areEqual);
        }

        [Fact]
        public void GetHashCode_SameObjectDifferentBulk_EqualHashCodes()
        {
            var firstTraverser = new Traverser("anObject", 1234);
            var secondTraverser = new Traverser("anObject", 9876);

            var firstHashCode = firstTraverser.GetHashCode();
            var secondHashCode = secondTraverser.GetHashCode();

            Assert.Equal(firstHashCode, secondHashCode);
        }
    }
}