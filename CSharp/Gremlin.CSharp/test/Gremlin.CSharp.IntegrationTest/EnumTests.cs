using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class EnumTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void SimpleTLabelTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.traversal().WithRemote(connection);

            var personsCount = g.V().Has(T.label, "person").Count().Next();

            Assert.Equal((long)4, personsCount);
        }
    }
}