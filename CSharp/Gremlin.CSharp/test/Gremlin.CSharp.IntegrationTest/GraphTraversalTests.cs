using System.Collections.Generic;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Structure;
using Xunit;
using static Gremlin.CSharp.Process.__;
using static Gremlin.CSharp.Process.Scope;

namespace Gremlin.CSharp.IntegrationTest
{
    public class GraphTraversalTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void g_V_Count_Test()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var count = g.V().Count().Next();

            Assert.Equal((long)6, count);
        }

        [Fact]
        public void NestedTraversalTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var t = g.V().Repeat(Out()).Times(2).Values("name");
            var names = t.ToList();

            Assert.Equal((long)2, names.Count);
            Assert.Contains("lop", names);
            Assert.Contains("ripple", names);
        }

        [Fact]
        public void GetValueMapTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var receivedValueMap = g.V().Has("name", "marko").ValueMap().Next();

            var expectedValueMap = new Dictionary<string, dynamic>
            {
                {"age", new List<object> {29}},
                {"name", new List<object> {"marko"}}
            };
            Assert.Equal(expectedValueMap, receivedValueMap);
        }

        [Fact]
        public void ShortestPathTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var shortestPath =
                (Path) g.V(5).Repeat(Both().SimplePath()).Until(HasId(6)).Limit(1).Path().Next();

            Assert.Equal((long)4, shortestPath.Count);
            Assert.Equal(new Vertex((long) 6), shortestPath[3]);
        }
    }
}
