using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest.DriverRemoteConnection
{
    public class PredicateTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void PredicateAndTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var count = g.V().Has("age", P.Gt(30).And(P.Lt(35))).Count().Next();

            Assert.Equal((long) 1, count);
        }

        [Fact]
        public void SimplePWithinTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var count = g.V().Has("name", P.Within("josh", "vadas")).Count().Next();

            Assert.Equal((long) 2, count);
        }
    }
}