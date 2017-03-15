using System.Collections.Generic;
using System.Linq;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using Xunit;
using static Gremlin.CSharp.Process.__;
using static Gremlin.CSharp.Process.P;

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

            Assert.Equal((long) 6, count);
        }

        [Fact]
        public void g_VX1X_Next_Test()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var vertex = (Vertex) g.V(1).Next();

            Assert.Equal(new Vertex((long) 1), vertex);
            Assert.Equal((long) 1, vertex.Id);
        }

        [Fact]
        public void g_VX1X_NextTraverser_Test()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var traverser = g.V(1).NextTraverser();

            Assert.Equal(new Traverser(new Vertex((long)1)), traverser);
        }

        [Fact]
        public void g_VX1X_ToList_Test()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var list = g.V(1).ToList();

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Next_WithAmountArgument_AmountNrValues()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var result = g.V().Repeat(Both()).Times(5).Next(10);

            Assert.Equal(10, result.Count());
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
        public void g_V_RepeatXOutX_TimesX2X_ValuesXNameX_Test()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var t = g.V().Repeat(Out()).Times(2).Values("name");
            var names = t.ToList();

            Assert.Equal((long) 2, names.Count);
            Assert.Contains("lop", names);
            Assert.Contains("ripple", names);
        }

        [Fact]
        public void WithSideEffect_TraversalUsingSideEffects_IncludeInResults()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var results =
                g.WithSideEffect("a", new List<string> {"josh", "peter"})
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

        [Fact]
        public void ShortestPathTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var shortestPath =
                (Path) g.V(5).Repeat(Both().SimplePath()).Until(HasId(6)).Limit(1).Path().Next();

            Assert.Equal((long) 4, shortestPath.Count);
            Assert.Equal(new Vertex((long) 6), shortestPath[3]);
        }

        [Fact]
        public void TraversalWithBindingsTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var b = new Bindings();
            var count = g.V().Has(b.Of("propertyKey", "name"), b.Of("propertyValue", "marko")).OutE().Count().Next();

            Assert.Equal((long) 3, count);
        }
    }
}