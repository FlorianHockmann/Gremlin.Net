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

        public Traversal Submit(Bytecode bytecode)
        {
            var resultSet = _client.SubmitAsync<Traverser>(bytecode).Result;

            return new DriverRemoteTraversal(_client, resultSet);
        }
    }
}