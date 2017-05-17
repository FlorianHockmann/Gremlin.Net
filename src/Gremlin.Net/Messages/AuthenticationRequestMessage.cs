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
using Newtonsoft.Json;

namespace Gremlin.Net.Messages
{
    /// <summary>
    /// Represents a authentication request message to send to a Gremlin Server.
    /// </summary>
    public class AuthenticationRequestMessage : RequestMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequestMessage"/> class.
        /// </summary>
        public AuthenticationRequestMessage()
        {
            this.Operation = "authentication";
        }

        /// <summary>
        /// Gets or sets parameters for this <see cref="AuthenticationRequestMessage"/> to pass to Gremlin Server.
        /// </summary>
        [JsonProperty(PropertyName = "args")]
        public AuthenticationRequestArguments Arguments { get; set; }
    }
}