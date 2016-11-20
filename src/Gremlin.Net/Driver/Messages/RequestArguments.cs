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

namespace Gremlin.Net.Driver.Messages
{
    /// <summary>
    /// Represents parameters to pass to Gremlin Server.
    /// </summary>
    public abstract class RequestArguments
    {
        /// <summary>
        /// Gets or set the number of iterations each ResponseMessage should contain.
        /// </summary>
        /// <value>
        /// When the result is an iterator this value defines the number of iterations each ResponseMessage should contain - overrides the resultIterationBatchSize server setting.
        /// </value>
        [JsonProperty(PropertyName = "batchSize", NullValueHandling = NullValueHandling.Ignore)]
        public int? BatchSize { get; set; }
    }
}