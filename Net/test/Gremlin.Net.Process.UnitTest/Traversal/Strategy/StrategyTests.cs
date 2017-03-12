using Gremlin.Net.Process.Traversal.Strategy;
using Gremlin.Net.Process.Traversal.Strategy.Optimization;
using Gremlin.Net.Process.Traversal.Strategy.Verification;
using Xunit;

namespace Gremlin.Net.Process.UnitTest.Traversal.Strategy
{
    public class StrategyTests
    {
        [Fact]
        public void StrategyName_TestStrategy_ClassName()
        {
            var testStrategy = new TestStrategy();

            Assert.Equal((string) "TestStrategy", (string) testStrategy.StrategyName);
        }

        [Fact]
        public void ToString_TestStrategy_StrategyName()
        {
            var testStrategy = new TestStrategy();

            var strategyStr = testStrategy.ToString();

            Assert.Equal("TestStrategy", strategyStr);
        }

        [Fact]
        public void Equals_EqualStrategyNameDifferentConfiguration_ReturnTrue()
        {
            var firstStrategy = new TestStrategy("aConfigKey", "aConfigValue");
            var secondStrategy = new TestStrategy("anotherKey", "anotherValue");

            var areEqual = firstStrategy.Equals(secondStrategy);

            Assert.True(areEqual);
        }

        [Fact]
        public void Equals_DifferentStrategyNames_ReturnFalse()
        {
            var firstStrategy = new TestStrategy("aConfigKey", "aConfigValue");
            var secondStrategy = new IncidentToAdjacentStrategy();

            var areEqual = firstStrategy.Equals(secondStrategy);

            Assert.False(areEqual);
        }

        [Fact]
        public void GetHashCode_EqualStrategyNameDifferentConfiguration_SameHashCode()
        {
            var firstStrategy = new TestStrategy("aConfigKey", "aConfigValue");
            var secondStrategy = new TestStrategy("anotherKey", "anotherValue");

            var firstHashCode = firstStrategy.GetHashCode();
            var secondHashCode = secondStrategy.GetHashCode();

            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Fact]
        public void GetHashCode_DifferentNameDifferentConfiguration_DifferentHashCode()
        {
            var firstStrategy = new TestStrategy();
            var secondStrategy = new ReadOnlyStrategy();

            var firstHashCode = firstStrategy.GetHashCode();
            var secondHashCode = secondStrategy.GetHashCode();

            Assert.NotEqual(firstHashCode, secondHashCode);
        }
    }

    internal class TestStrategy : AbstractTraversalStrategy
    {
        public TestStrategy()
        { }

        public TestStrategy(string configKey, dynamic configValue)
        {
            Configuration[configKey] = configValue;
        }
    }
}