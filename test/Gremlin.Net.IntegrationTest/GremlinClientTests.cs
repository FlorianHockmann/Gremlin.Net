#region License
/*
 * Copyright 2016 Florian Hockmann
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Exceptions;
using Gremlin.Net.IntegrationTest.Util;
using Gremlin.Net.Messages;
using Xunit;

namespace Gremlin.Net.IntegrationTest
{
    public class GremlinClientTests
    {
        private readonly ScriptRequestMessageProvider _requestMessageProvider = new ScriptRequestMessageProvider();
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        [Theory]
        [InlineData("1+1", "2")]
        [InlineData("'Hello' + 'World'", "HelloWorld")]
        public async Task ScriptShouldBeEvaluatedAndResultReturned(string requestMsg, string expectedResponse)
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var response = await gremlinClient.SubmitWithSingleResultAsync<string>(requestMsg);

                Assert.Equal(expectedResponse, response);
            }
        }

        [Fact]
        public async Task HandleBigResponseTest()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var responseMsgSize = 5000;
                var requestMsg = $"'1'*{responseMsgSize}";

                var response = await gremlinClient.SubmitWithSingleResultAsync<string>(requestMsg);

                Assert.Equal(responseMsgSize, response.Length);
            }
        }

        [Fact]
        public async Task InvalidScriptShouldThrowException()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = "invalid";

                var exception =
                    await Assert.ThrowsAsync<ResponseException>(() => gremlinClient.SubmitAsync(requestMsg));

                Assert.Equal(typeof(ResponseException), exception.GetType());
                Assert.Contains($"ScriptEvaluationError: No such property: {requestMsg}",
                    exception.Message);
            }
        }

        [Fact]
        public async Task ResponseBatchesShouldBeReassembled()
        {
            const int batchSize = 2;
            var expectedResult = new List<int> {1, 2, 3, 4, 5};
            var requestScript = $"{nameof(expectedResult)}";
            var bindings = new Dictionary<string, object> {{nameof(expectedResult), expectedResult}};
            var requestMessage = new ScriptRequestMessage
            {
                Arguments =
                    new ScriptRequestArguments
                    {
                        BatchSize = batchSize,
                        GremlinScript = requestScript,
                        Bindings = bindings
                    }
            };
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var response = await gremlinClient.SubmitAsync<int>(requestMessage);

                Assert.Equal(expectedResult, response);
            }
        }

        [Fact]
        public async Task ResponsesShouldBeCorrectlyAssignedToRequests()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var sleepTime = 100;
                var expectedFirstResult = 1;
                var firstRequestMsg = _requestMessageProvider.GetSleepMessage(sleepTime);
                firstRequestMsg.Arguments.GremlinScript += $"{expectedFirstResult}";
                var expectedSecondResponse = 2;
                var secondScript = $"{expectedSecondResponse}";

                var firstResponseTask = gremlinClient.SubmitWithSingleResultAsync<int>(firstRequestMsg);
                var secondResponseTask = gremlinClient.SubmitWithSingleResultAsync<int>(secondScript);

                var secondResponse = await secondResponseTask;
                Assert.Equal(expectedSecondResponse, secondResponse);
                var firstResponse = await firstResponseTask;
                Assert.Equal(expectedFirstResult, firstResponse);
            }
        }

        [Fact]
        public async Task ResponsesShouldBeEnumerable()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var expectedResult = new List<int> {1, 2, 3, 4, 5};
                var requestMsg = $"{nameof(expectedResult)}";
                var bindings = new Dictionary<string, object> {{nameof(expectedResult), expectedResult}};

                var response = await gremlinClient.SubmitAsync<int>(requestMsg, bindings);

                Assert.Equal(expectedResult, response);
            }
        }

        [Fact]
        public async Task SimpleInvalidScriptShouldThrowExceptionOnExecution()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var invalidRequestScript = "invalid";

                await Assert.ThrowsAsync<ResponseException>(() => gremlinClient.SubmitAsync(invalidRequestScript));
            }
        }

        [Fact]
        public async Task SimpleScriptShouldBeExecutedWithoutErrors()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = _requestMessageProvider.GetDummyMessage();

                await gremlinClient.SubmitAsync(requestMsg);
            }
        }

        [Fact]
        public async Task UseBindingsForScript()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = "a + b";
                var a = 1;
                var b = 2;
                var bindings = new Dictionary<string, object> {{"a", a}, {"b", b}};

                var response = await gremlinClient.SubmitWithSingleResultAsync<int>(requestMsg, bindings);

                Assert.Equal(a + b, response);
            }
        }

        [Fact]
        public async Task HandleResponseWithoutContent()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var gremlinScript = "g.V().has(propertyKey, propertyValue);";
                var bindings = new Dictionary<string, object>
                {
                    {"propertyKey", "name"},
                    {"propertyValue", "unknownTestName"}
                };

                var response = await gremlinClient.SubmitWithSingleResultAsync<object>(gremlinScript, bindings);

                Assert.Null(response);
            }
        }
    }
}