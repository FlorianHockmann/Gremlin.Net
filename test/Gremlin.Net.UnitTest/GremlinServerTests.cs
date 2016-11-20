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

using Gremlin.Net.Driver;
using Xunit;

namespace Gremlin.Net.UnitTest
{
    public class GremlinServerTests
    {
        [Theory]
        [InlineData("localhost", 8182)]
        [InlineData("1.2.3.4", 5678)]
        public void BuildCorrectUri(string host, int port)
        {
            var gremlinServer = new GremlinServer(host, port);

            var uri = gremlinServer.Uri;

            Assert.Equal($"ws://{host}:{port}/gremlin", uri.AbsoluteUri);
        }

        [Fact]
        public void BuildCorrectUriForSsl()
        {
            var host = "localhost";
            var port = 8181;
            var gremlinServer = new GremlinServer(host, port, true);

            var uri = gremlinServer.Uri;

            Assert.Equal($"wss://{host}:{port}/gremlin", uri.AbsoluteUri);
        }

        [Fact]
        public void UseCorrectDefaultPortIfNoneProvided()
        {
            var host = "testHost";
            var gremlinServer = new GremlinServer(host);

            var uri = gremlinServer.Uri;

            Assert.Equal(8182, uri.Port);
        }
    }
}