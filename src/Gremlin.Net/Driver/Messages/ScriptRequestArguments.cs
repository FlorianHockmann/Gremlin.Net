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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gremlin.Net.Driver.Messages
{
    /// <summary>
    /// Represents parameters to pass to Gremlin Server for a <see cref="ScriptRequestMessage"/>.
    /// </summary>
    public class ScriptRequestArguments : RequestArguments
    {
        /// <summary>
        /// Gets or sets the Gremlin script to evaluate.
        /// </summary>
        /// <value>A <see cref="string"/> that contains the Gremlin script to evaluate.</value>
        [JsonProperty(PropertyName = "gremlin")]
        public string GremlinScript { get; set; }

        /// <summary>
        /// Gets or sets the bindings for the <see cref="GremlinScript"/>.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey, TValue}"/> to apply as variables in the context of the <see cref="GremlinScript"/>.</value>
        [JsonProperty(PropertyName = "bindings", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Bindings { get; set; }

        /// <summary>
        /// Gets or sets the flavor of Gremlin used (e.g. gremlin-groovy).
        /// </summary>
        /// <value>The default value is null which results in the default value of gremlin-groovy being used.</value>
        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets aliases that allow to bind names of Graph and TraversalSource to other names.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey, TValue}"/> that allows globally bound Graph and TraversalSource objects to be aliased to different variable names for purposes of the current request. The value represents the name of the global variable and its key represents the new binding name as it will be referenced in the Gremlin query. For example, if the Gremlin Server defines two TraversalSource instances named g1 and g2, it would be possible to send an alias pair with key of "g" and value of "g2" and thus allow the script to refer to "g2" simply as "g".</value>
        [JsonProperty(PropertyName = "aliases", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Aliases { get; set; }

        /// <summary>
        /// Gets or sets the override for the server setting that determines the maximum time to wait for a script to execute on the server.
        /// </summary>
        /// <value>The evaluation timeout value in milliseconds for this request message. The value can be null which avoids overriding the server setting.</value>
        [JsonProperty(PropertyName = "scriptEvaluationTimeout", NullValueHandling = NullValueHandling.Ignore)]
        public long? ScriptEvaluationTimeoutInMs { get; set; }
    }
}