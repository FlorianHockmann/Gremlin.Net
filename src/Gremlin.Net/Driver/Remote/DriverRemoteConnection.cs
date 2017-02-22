using System.Threading.Tasks;
using Gremlin.Net.Process.Remote;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    public class DriverRemoteConnection : IRemoteConnection
    {
        private readonly IGremlinClient _client;

        public DriverRemoteConnection(IGremlinClient client)
        {
            _client = client;
        }

        public async Task<Traversal> SubmitAsync(Bytecode bytecode)
        {
            var resultSet = await _client.SubmitAsync<Traverser>(bytecode).ConfigureAwait(false);

            return new DriverRemoteTraversal(_client, resultSet);
        }
    }
}