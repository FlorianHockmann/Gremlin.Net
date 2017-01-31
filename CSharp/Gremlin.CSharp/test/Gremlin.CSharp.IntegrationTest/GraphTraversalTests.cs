using System;
using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.IntegrationTest;
using Gremlin.Net.Process.Remote;
using Xunit;

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

            Assert.Equal((long)0, count);
        }

        private IRemoteConnection CreateRemoteConnection()
        {
            return new DriverRemoteConnection(new GremlinClient(new GremlinServer(TestHost, TestPort)));
        }
    }
}
