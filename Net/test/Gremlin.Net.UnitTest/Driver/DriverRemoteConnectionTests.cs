using System;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Moq;
using Xunit;

namespace Gremlin.Net.UnitTest.Driver
{
    public class DriverRemoteConnectionTests
    {
        [Fact]
        public void Dispose_ProvidedGremlinClient_DisposeGremlinClient()
        {
            var gremlinClientMock = new Mock<IGremlinClient>();
            var driverRemoteConnection = new DriverRemoteConnection(gremlinClientMock.Object);

            driverRemoteConnection.Dispose();

            gremlinClientMock.Verify(m => m.Dispose());
        }

        [Fact]
        public void Constructor_GivenNullAsGremlinClient_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new DriverRemoteConnection(null));
        }
    }
}