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

namespace Gremlin.Net.Messages
{
    public class ScriptRequestArguments : RequestArguments
    {
        [JsonProperty(PropertyName = "gremlin")]
        public string GremlinScript { get; set; }

        [JsonProperty(PropertyName = "bindings", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Bindings { get; set; }

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "aliases", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Aliases { get; set; }

        [JsonProperty(PropertyName = "scriptEvaluationTimeout", NullValueHandling = NullValueHandling.Ignore)]
        public long? ScriptEvaluationTimeoutInMs { get; set; }
    }
}