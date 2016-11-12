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
using System.Linq;
using System.Threading.Tasks;
using Gremlin.Net.Messages;

namespace Gremlin.Net
{
    public static class GremlinClientExtensions
    {
        public static async Task<T> SubmitWithSingleResultAsync<T>(this IGremlinClient gremlinClient,
            string requestScript,
            Dictionary<string, object> bindings = null)
        {
            var resultCollection = await gremlinClient.SubmitAsync<T>(requestScript, bindings).ConfigureAwait(false);
            return resultCollection.FirstOrDefault();
        }

        public static async Task<T> SubmitWithSingleResultAsync<T>(this IGremlinClient gremlinClient,
            ScriptRequestMessage requestMessage)
        {
            var resultCollection = await gremlinClient.SubmitAsync<T>(requestMessage).ConfigureAwait(false);
            return resultCollection.FirstOrDefault();
        }

        public static async Task SubmitAsync(this IGremlinClient gremlinClient, string requestScript,
            Dictionary<string, object> bindings = null)
        {
            await gremlinClient.SubmitAsync<object>(requestScript, bindings).ConfigureAwait(false);
        }

        public static async Task SubmitAsync(this IGremlinClient gremlinClient, ScriptRequestMessage requestMessage)
        {
            await gremlinClient.SubmitAsync<object>(requestMessage).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<T>> SubmitAsync<T>(this IGremlinClient gremlinClient, string requestScript,
            Dictionary<string, object> bindings = null)
        {
            var requestMessage = new ScriptRequestMessage
            {
                Arguments = new ScriptRequestArguments {GremlinScript = requestScript, Bindings = bindings}
            };
            return await gremlinClient.SubmitAsync<T>(requestMessage).ConfigureAwait(false);
        }
    }
}