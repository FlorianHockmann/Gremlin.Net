using System.Collections.Generic;
using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest.DriverRemoteConnection
{
    public class GraphTraversalSourceTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void WithSideEffects_ListAsSideEffect_IncludeInBytecode()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var results = g.WithSideEffect("a", new List<string> {"josh", "peter"})
                .V(1)
                .Out("created")
                .In("created")
                .Values("name")
                .Where(P.Within("a"))
                .ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains("josh", results);
            Assert.Contains("peter", results);
        }
    }
}