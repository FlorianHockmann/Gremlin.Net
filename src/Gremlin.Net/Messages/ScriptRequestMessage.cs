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

using Newtonsoft.Json;

namespace Gremlin.Net.Messages
{
    /// <summary>
    /// Represents a script request message to send to a Gremlin Server.
    /// </summary>
    public class ScriptRequestMessage : RequestMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptRequestMessage"/> class.
        /// </summary>
        public ScriptRequestMessage()
        {
            Operation = "eval";
        }

        /// <summary>
        /// Gets or sets parameters for this <see cref="ScriptRequestMessage"/> to pass to Gremlin Server.
        /// </summary>
        [JsonProperty(PropertyName = "args")]
        public ScriptRequestArguments Arguments { get; set; }
    }
}