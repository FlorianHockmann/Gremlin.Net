using System;
using Gremlin.Net.Driver;
using Gremlin.Net.IntegrationTest;
using Gremlin.Net.Process.Remote;

namespace Gremlin.CSharp.IntegrationTest.DriverRemoteConnection
{
    internal class RemoteConnectionFactory
    {
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        public IRemoteConnection CreateRemoteConnection()
        {
            return new Net.Driver.Remote.DriverRemoteConnection(new GremlinClient(new GremlinServer(TestHost, TestPort)));
        }
    }
}