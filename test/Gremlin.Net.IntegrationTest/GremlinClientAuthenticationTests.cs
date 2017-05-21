using System;
using System.Threading.Tasks;
using Gremlin.Net.Exceptions;
using Gremlin.Net.IntegrationTest.Util;
using Xunit;

namespace Gremlin.Net.IntegrationTest
{
    public class GremlinClientAuthenticationTests
    {
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestSecureServerPort"]);
        private readonly ScriptRequestMessageProvider _requestMessageProvider = new ScriptRequestMessageProvider();

        [Fact]
        public async Task ShouldThrowForMissingCredentials()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await gremlinClient.SubmitWithSingleResultAsync<string>(_requestMessageProvider
                        .GetDummyMessage()));

                Assert.Contains("authentication", exception.Message);
                Assert.Contains("credentials", exception.Message);
            }
        }

        [Theory]
        [InlineData("unknownUser", "passwordDoesntMatter")]
        [InlineData("stephen", "wrongPassword")]
        public async Task ShouldThrowForWrongCredentials(string username, string password)
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort, username: username, password: password);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var exception = await Assert.ThrowsAsync<ResponseException>(
                    async () => await gremlinClient.SubmitWithSingleResultAsync<string>(_requestMessageProvider
                        .GetDummyMessage()));

                Assert.Contains("Unauthorized", exception.Message);
            }
        }

        [Theory]
        [InlineData("1+1", "2")]
        [InlineData("'Hello' + 'World'", "HelloWorld")]
        public async Task ScriptShouldBeEvaluatedAndResultReturnedForCorrectCredentials(string requestMsg,
            string expectedResponse)
        {
            const string username = "stephen";
            const string password = "password";
            var gremlinServer = new GremlinServer(TestHost, TestPort, username: username, password: password);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var response = await gremlinClient.SubmitWithSingleResultAsync<string>(requestMsg);

                Assert.Equal(expectedResponse, response);
            }
        }
    }
}