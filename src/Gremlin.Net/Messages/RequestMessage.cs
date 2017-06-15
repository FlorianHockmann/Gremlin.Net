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
    /// Represents a script request message to send to a Gremlin Server.
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// Gets the ID of this request message.
        /// </summary>
        /// <value>A UUID representing the unique identification for the request.</value>
        [JsonProperty(PropertyName = "requestId")]
        public Guid RequestId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the operation that should be executed by the Gremlin Server.
        /// </summary>
        /// <value>The name of the "operation" to execute based on the available OpProcessor configured in the Gremlin Server.</value>
        [JsonProperty(PropertyName = "op")]
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the name of the OpProcessor to utilize.
        /// </summary>
        /// <value>The name of the OpProcessor to utilize. This defaults to an empty string which represents the default OpProcessor for evaluating scripts.</value>
        [JsonProperty(PropertyName = "processor")]
        public string Processor { get; set; } = "";
    }
}