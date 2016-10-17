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
using System.Diagnostics;
using Gremlin.Net.Exceptions;
using Gremlin.Net.IntegrationTest.Util;
using Gremlin.Net.Messages;
using Xunit;

namespace Gremlin.Net.IntegrationTest
{
    public class MessagesTests
    {
        private readonly ScriptRequestMessageProvider _requestMessageProvider = new ScriptRequestMessageProvider();
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);

        [Fact]
        public void AliasForTraversalSourceShouldBeUsed()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var aliasTraversalSource = "g2";
                var aliases = new Dictionary<string, string> {{aliasTraversalSource, "g"}};
                var gremlinScript = $"{aliasTraversalSource}.V().count()";
                var requestMsg = new ScriptRequestMessage
                {
                    Arguments = new ScriptRequestArguments {Aliases = aliases, GremlinScript = gremlinScript}
                };

                var result = gremlinClient.SubmitWithSingleResultAsync<bool>(requestMsg).Result;

                Assert.NotNull(result);
            }
        }

        [Fact]
        public void InvalidOperationShouldThrowException()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var ivalidOperationName = "invalid";
                var requestMsg = _requestMessageProvider.GetDummyMessage();
                requestMsg.Operation = ivalidOperationName;

                var thrownException =
                    Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(requestMsg).Wait());

                var innerException = thrownException.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
                Assert.Contains(ResponseStatusCode.MalformedRequest.ToString(), innerException.Message);
                Assert.Contains(ivalidOperationName, innerException.Message);
            }
        }

        [Fact]
        public void InvalidProcessorShouldThrowException()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var invalidProcessorName = "invalid";
                var requestMsg = _requestMessageProvider.GetDummyMessage();
                requestMsg.Processor = invalidProcessorName;

                var thrownException =
                    Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(requestMsg).Wait());

                var innerException = thrownException.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
                Assert.Contains(ResponseStatusCode.InvalidRequestArguments.ToString(), innerException.Message);
                Assert.Contains(invalidProcessorName, innerException.Message);
                Assert.Contains("OpProcessor", innerException.Message);
            }
        }

        [Fact]
        public void ScriptEvaluationTimeoutShouldBeConfigurable()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                const int timeOutInMs = 1;
                const int scriptSleepTimeInMs = 5000;
                var requestMsg = _requestMessageProvider.GetSleepMessage(scriptSleepTimeInMs);
                requestMsg.Arguments.ScriptEvaluationTimeoutInMs = timeOutInMs;
                var evaluationStopWatch = new Stopwatch();
                evaluationStopWatch.Start();

                var thrownException = Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(requestMsg).Wait());

                evaluationStopWatch.Stop();
                var innerException = thrownException.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
                Assert.Contains(ResponseStatusCode.ServerTimeout.ToString(), innerException.Message);
                Assert.Contains(timeOutInMs.ToString(), innerException.Message);
                Assert.True(evaluationStopWatch.ElapsedMilliseconds < timeOutInMs*1000);
            }
        }

        [Fact]
        public void UnsupportedLanguageShouldThrowException()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var unknownLanguage = "unknown";
                var requestMsg = _requestMessageProvider.GetDummyMessage();
                requestMsg.Arguments.Language = unknownLanguage;

                var thrownException =
                    Assert.Throws<AggregateException>(() => gremlinClient.SubmitAsync(requestMsg).Wait());

                var innerException = thrownException.InnerException;
                Assert.Equal(typeof(ResponseException), innerException.GetType());
                Assert.Contains(ResponseStatusCode.ScriptEvaluationError.ToString(), innerException.Message);
                Assert.Contains(unknownLanguage, innerException.Message);
                Assert.Contains("Language", innerException.Message);
            }
        }
    }
}