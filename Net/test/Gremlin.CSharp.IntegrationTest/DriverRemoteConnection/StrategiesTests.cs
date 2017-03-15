using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest.DriverRemoteConnection
{
    public class StrategiesTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void g_V_Count_Next_WithVertexLabelSubgraphStrategy_CountOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.HasLabel("person")));

            var count = g.V().Count().Next();

            Assert.Equal((long) 4, count);
        }

        [Fact]
        public void g_E_Count_Next_WithVertexAndEdgeLabelSubgraphStrategy_CountOfSubgraphEdges()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.HasLabel("person"),
                        edgeCriterion: __.HasLabel("created")));

            var count = g.E().Count().Next();

            Assert.Equal((long)0, count);
        }

        [Fact]
        public void g_V_Label_Dedup_Count_Next_WithVertexLabelSubgraphStrategy_CountLabelsOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.HasLabel("person")));

            var count = g.V().Label().Dedup().Count().Next();

            Assert.Equal((long)1, count);
        }

        [Fact]
        public void g_V_Label_Dedup_Next_WWithVertexLabelSubgraphStrategy_LabelOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.HasLabel("person")));

            var label = g.V().Label().Dedup().Next();

            Assert.Equal("person", label);
        }

        [Fact]
        public void g_V_Count_Next_WithVertexHasPropertySubgraphStrategy_CountOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.Has("name", "marko")));

            var count = g.V().Count().Next();

            Assert.Equal((long)1, count);
        }

        [Fact]
        public void g_E_Count_Next_WithEdgeLimitSubgraphStrategy_CountOfSubgraphEdges()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(edgeCriterion: __.Limit(0)));

            var count = g.E().Count().Next();

            Assert.Equal((long)0, count);
        }

        [Fact]
        public void g_V_Label_Dedup_Next_WithVertexHasPropertySubgraphStrategy_LabelOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.Has("name", "marko")));

            var label = g.V().Label().Dedup().Next();

            Assert.Equal("person", label);
        }

        [Fact]
        public void g_V_ValuesXnameX_Next_WithVertexHasPropertySubgraphStrategy_NameValueOfSubgraphVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g =
                graph.Traversal()
                    .WithRemote(connection)
                    .WithStrategies(new SubgraphStrategy(vertexCriterion: __.Has("name", "marko")));

            var name = g.V().Values("name").Next();

            Assert.Equal("marko", name);
        }

        [Fact]
        public void g_V_Count_Next_WithComputer_CountOfVertices()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection).WithComputer();

            var count = g.V().Count().Next();

            Assert.Equal((long)6, count);
        }

        [Fact]
        public void g_E_Count_Next_WithComputer_CountOfEdges()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection).WithComputer();

            var count = g.E().Count().Next();

            Assert.Equal((long)6, count);
        }
    }
}