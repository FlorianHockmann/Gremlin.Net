using System.Collections.Generic;
using Gremlin.CSharp.Structure;
using Xunit;
using static Gremlin.CSharp.Process.P;

namespace Gremlin.CSharp.IntegrationTest
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
                .Where(Within("a"))
                .ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains("josh", results);
            Assert.Contains("peter", results);
        }
    }
}