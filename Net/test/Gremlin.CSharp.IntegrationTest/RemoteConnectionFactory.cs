using System;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.IntegrationTest;
using Gremlin.Net.Process.Remote;

namespace Gremlin.CSharp.IntegrationTest
{
    internal class RemoteConnectionFactory
    {
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        public IRemoteConnection CreateRemoteConnection()
        {
            return new DriverRemoteConnection(new GremlinClient(new GremlinServer(TestHost, TestPort)));
        }
    }
}