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
using System.Text;
using Gremlin.Net.Messages;
using Xunit;

namespace Gremlin.Net.UnitTest
{
    public class MessagesTests
    {
        [Fact]
        public void RequestIdsShouldBeUnique()
        {
            var firstMsg = new RequestMessage();
            var secondMsg = new RequestMessage();

            Assert.NotEqual(firstMsg.RequestId, secondMsg.RequestId);
        }

        [Fact]
        public void RequestIdShouldNotChangeAfterBeingCreated()
        {
            var requestMsg = new RequestMessage();

            var requestIdOnFirstAccess = requestMsg.RequestId;
            var requestIdOnSecondAccess = requestMsg.RequestId;

            Assert.Equal(requestIdOnFirstAccess, requestIdOnSecondAccess);
        }

        [Theory]
        [InlineData("username", "password")]
        public void BuildCorrectSasl(string username, string password)
        {
            var expectedSaslPlaintext = $"\0{username}\0{password}";

            var argument = new AuthenticationRequestArguments(username, password);

            Assert.Equal(Base64Encode(expectedSaslPlaintext), argument.Sasl);
        }

        private string Base64Encode(string plaintext)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(plaintextBytes);
        }
    }
}