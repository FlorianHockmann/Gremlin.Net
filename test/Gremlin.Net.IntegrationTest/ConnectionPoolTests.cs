﻿#region License
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
using Gremlin.Net.IntegrationTest.Util;
using Xunit;

namespace Gremlin.Net.IntegrationTest
{
    public class ConnectionPoolTests
    {
        private readonly ScriptRequestMessageProvider _requestMessageProvider = new ScriptRequestMessageProvider();
        private static readonly string TestHost = ConfigProvider.Configuration["TestServerIpAddress"];
        private static readonly int TestPort = Convert.ToInt32(ConfigProvider.Configuration["TestServerPort"]);
        
        [Fact]
        public async Task ConnectionShouldBeReusedForSequentialRequests()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                await gremlinClient.SubmitAsync("");
                await gremlinClient.SubmitAsync("");

                var nrConnections = gremlinClient.NrConnections;
                Assert.Equal(1, nrConnections);
            }
        }

        [Fact]
        public void ConnectionShouldOnlyBeCreatedIfNeeded()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var nrConnections = gremlinClient.NrConnections;
                Assert.Equal(0, nrConnections);
            }
        }

        [Fact]
        public async Task ParallelRequestsShouldBeExecutedOnDifferentConnections()
        {
            var gremlinServer = new GremlinServer(TestHost, TestPort);
            using (var gremlinClient = new GremlinClient(gremlinServer))
            {
                var sleepTime = 50;
                var nrParallelRequests = 5;

                await ExecuteMultipleLongRunningRequestsInParallel(gremlinClient, nrParallelRequests, sleepTime);

                var nrConnections = gremlinClient.NrConnections;
                Assert.Equal(nrParallelRequests, nrConnections);
            }
        }

        private async Task ExecuteMultipleLongRunningRequestsInParallel(IGremlinClient gremlinClient, int nrRequests,
            int requestRunningTimeInMs)
        {
            var longRunningRequestMsg = _requestMessageProvider.GetSleepMessage(requestRunningTimeInMs);
            var tasks = new List<Task>(nrRequests);
            for (var i = 0; i < nrRequests; i++)
                tasks.Add(gremlinClient.SubmitAsync(longRunningRequestMsg));
            await Task.WhenAll(tasks);
        }
    }
}