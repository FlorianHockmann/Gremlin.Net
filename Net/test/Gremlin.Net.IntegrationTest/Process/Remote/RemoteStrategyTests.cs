using System;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Remote;
using Gremlin.Net.Process.Traversal;
using Xunit;

namespace Gremlin.Net.IntegrationTest.Process.Remote
{
    public class RemoteStrategyTests
    {
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        [Fact]
        public void ShouldSendBytecodeToGremlinServer()
        {
            const string expectedResult = "gremlin";
            var testBytecode = new Bytecode();
            testBytecode.AddStep("V");
            testBytecode.AddStep("has", "test");
            testBytecode.AddStep("inject", expectedResult);
            var testTraversal = CreateTraversalWithRemoteStrategy(testBytecode);

            var actualResult = testTraversal.Next();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ShouldSendBytecodeToGremlinServerAsynchronouslyForTraversalPromise()
        {
            const string expectedResult = "gremlin";
            var testBytecode = new Bytecode();
            testBytecode.AddStep("V");
            testBytecode.AddStep("has", "test");
            testBytecode.AddStep("inject", expectedResult);
            var testTraversal = CreateTraversalWithRemoteStrategy(testBytecode);

            var actualResult = await testTraversal.Promise(t => t.Next());

            Assert.Equal(expectedResult, actualResult);
        }

        private Traversal CreateTraversalWithRemoteStrategy(Bytecode bytecode)
        {
            var remoteStrategy =
                new RemoteStrategy(new DriverRemoteConnection(new GremlinClient(new GremlinServer(TestHost, TestPort))));
            return new TestTraversal(remoteStrategy, bytecode);
        }
    }

    internal class TestTraversal : Traversal
    {
        public TestTraversal(ITraversalStrategy traversalStrategy, Bytecode bytecode)
        {
            TraversalStrategies.Add(traversalStrategy);
            Bytecode = bytecode;
        }
    }
}