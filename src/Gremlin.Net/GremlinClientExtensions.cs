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

        public static async Task<IList<T>> SubmitAsync<T>(this IGremlinClient gremlinClient, string requestScript,
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