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
using Gremlin.Net.Exceptions;
using Gremlin.Net.IntegrationTest.Properties;
using Gremlin.Net.IntegrationTest.Util;
using Gremlin.Net.Messages;
using Xunit;

namespace Gremlin.Net.IntegrationTest
{
    public class GremlinClientTests
    {
        private readonly ScriptRequestMessageProvider _requestMessageProvider = new ScriptRequestMessageProvider();
        private static readonly string TestHost = Settings.Default.TestServerIpAddress;
        private static readonly int TestPort = Settings.Default.TestServerPort;

        [Theory]
        [InlineData("1+1", "2")]
        [InlineData("'Hello' + 'World'", "HelloWorld")]
        public void ScriptShouldBeEvaluatedAndResultReturned(string requestMsg, string expectedResponse)
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var response = gremlinClient.SubmitWithSingleResultAsync<string>(requestMsg).Result;

                Assert.Equal(expectedResponse, response);
            }
        }

        [Fact]
        public void HandleBigResponseTest()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var responseMsgSize = 5000;
                var requestMsg = $"'1'*{responseMsgSize}";

                var response = gremlinClient.SubmitWithSingleResultAsync<string>(requestMsg).Result;

                Assert.Equal(responseMsgSize, response.Length);
            }
        }

        [Fact]
        public void InvalidScriptShouldThrowException()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = "invalid";

                var exception = Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(requestMsg).Wait());

                var innerException = exception.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
                Assert.Contains($"{ResponseStatusCode.ScriptEvaluationError}: No such property: {requestMsg}",
                    innerException.Message);
            }
        }

        [Fact]
        public void ResponseBatchesShouldBeReassembled()
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
                var response = gremlinClient.SubmitAsync<int>(requestMessage).Result;

                Assert.Equal(expectedResult, response);
            }
        }

        [Fact]
        public void ResponsesShouldBeCorrectlyAssignedToRequests()
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

                var secondResponse = secondResponseTask.Result;
                Assert.Equal(expectedSecondResponse, secondResponse);
                var firstResponse = firstResponseTask.Result;
                Assert.Equal(expectedFirstResult, firstResponse);
            }
        }

        [Fact]
        public void ResponsesShouldBeEnumerable()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var expectedResult = new List<int> {1, 2, 3, 4, 5};
                var requestMsg = $"{nameof(expectedResult)}";
                var bindings = new Dictionary<string, object> {{nameof(expectedResult), expectedResult}};

                var response = gremlinClient.SubmitAsync<int>(requestMsg, bindings).Result;

                Assert.Equal(expectedResult, response);
            }
        }

        [Fact]
        public void SimpleInvalidScriptShouldThrowExceptionOnExecution()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var invalidRequestScript = "invalid";

                var exceptionThrown =
                    Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(invalidRequestScript).Wait());

                var innerException = exceptionThrown.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
            }
        }

        [Fact]
        public void SimpleScriptShouldBeExecutedWithoutErrors()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = _requestMessageProvider.GetDummyMessage();

                var responseTask = gremlinClient.SubmitAsync(requestMsg);

                responseTask.Wait();
            }
        }

        [Fact]
        public void UseBindingsForScript()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var requestMsg = "a + b";
                var a = 1;
                var b = 2;
                var bindings = new Dictionary<string, object> {{"a", a}, {"b", b}};

                var response = gremlinClient.SubmitWithSingleResultAsync<int>(requestMsg, bindings).Result;

                Assert.Equal(a + b, response);
            }
        }

        [Fact]
        public void HandleResponseWithoutContent()
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

                var response = gremlinClient.SubmitWithSingleResultAsync<object>(gremlinScript, bindings).Result;

                Assert.Null(response);
            }
        }
    }
}