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
using Newtonsoft.Json;

namespace Gremlin.Net.Messages
{
    /// <summary>
    /// Represents parameters to pass to Gremlin Server for a <see cref="AuthenticationRequestMessage"/>.
    /// </summary>
    public class AuthenticationRequestArguments : RequestArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequestArguments"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public AuthenticationRequestArguments(string username, string password)
        {
            var auth = $"\0{username}\0{password}";
            var authBytes = Encoding.UTF8.GetBytes(auth);
            Sasl = Convert.ToBase64String(authBytes);
        }

        /// <summary>
        /// Gets or sets the response to the server authentication challenge. This value is dependent on the SASL authentication mechanism required by the server.
        /// </summary>
        /// <value>The response to the server authentication challenge.</value>
        [JsonProperty(PropertyName = "sasl")]
        public string Sasl { get; set; }
    }
}