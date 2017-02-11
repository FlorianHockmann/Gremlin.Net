using System;
using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.IntegrationTest;
using Gremlin.Net.Process.Remote;
using Xunit;
using static Gremlin.CSharp.Process.__;

namespace Gremlin.CSharp.IntegrationTest
{
    public class GraphTraversalTests
    {
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        [Fact]
        public void g_V_Count_Test()
        {
            var graph = new Graph();
            var connection = CreateRemoteConnection();
            var g = graph.traversal().WithRemote(connection);

            var count = g.V().Count().Next();

            Assert.Equal((long)6, count);
        }

        [Fact]
        public void NestedTraversalTest()
        {
            var graph = new Graph();
            var connection = CreateRemoteConnection();
            var g = graph.traversal().WithRemote(connection);

            var t = g.V().Repeat(Out()).Times(2).Values("name");
            var names = t.ToList();

            Assert.Equal((long)2, names.Count);
            Assert.Contains("lop", names);
            Assert.Contains("ripple", names);
        }

        private IRemoteConnection CreateRemoteConnection()
        {
            return new DriverRemoteConnection(new GremlinClient(new GremlinServer(TestHost, TestPort)));
        }
    }
}
