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

namespace Gremlin.NetCore.Messages
{
    public class ScriptRequestMessage
    {
        [JsonProperty(PropertyName = "requestId")]
        public Guid RequestId => Guid.NewGuid();

        [JsonProperty(PropertyName = "op")]
        public string Operation { get; set; } = "eval";

        [JsonProperty(PropertyName = "processor")]
        public string Processor { get; set; } = "";

        [JsonProperty(PropertyName = "args")]
        public ScriptRequestArguments Arguments { get; set; }
    }
}