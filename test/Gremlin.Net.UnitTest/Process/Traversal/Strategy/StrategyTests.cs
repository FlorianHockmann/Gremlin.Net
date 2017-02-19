using System.Collections.Generic;
using Gremlin.Net.Process.Traversal.Strategy;
using Gremlin.Net.Process.Traversal.Strategy.Optimization;
using Xunit;

namespace Gremlin.Net.UnitTest.Process.Traversal.Strategy
{
    public class StrategyTests
    {
        [Fact]
        public void StrategyName_TestStrategy_ClassName()
        {
            var testStrategy = new TestStrategy();

            Assert.Equal("TestStrategy", testStrategy.StrategyName);
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