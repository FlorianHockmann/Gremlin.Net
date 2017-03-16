using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Process.Remote;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    public class DriverRemoteConnection : IRemoteConnection, IDisposable
    {
        private readonly IGremlinClient _client;

        /// <summary>
        /// Initializes a new DriverRemoteConnection.
        /// </summary>
        /// <param name="client">The client that will be used for the connection.</param>
        /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
        public DriverRemoteConnection(IGremlinClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Traversal> SubmitAsync(Bytecode bytecode)
        {
            var requestId = Guid.NewGuid();
            var resultSet = await SubmitBytecodeAsync(requestId, bytecode).ConfigureAwait(false);
            return new DriverRemoteTraversal(_client, requestId, resultSet);
        }

        private async Task<IEnumerable<Traverser>> SubmitBytecodeAsync(Guid requestid, Bytecode bytecode)
        {
            var requestMsg =
                RequestMessage.Build(Tokens.OpsBytecode)
                    .Processor(Tokens.ProcessorTraversal)
                    .OverrideRequestId(requestid)
                    .AddArgument(Tokens.ArgsGremlin, bytecode)
                    .AddArgument(Tokens.ArgsAliases, new Dictionary<string, string> {{"g", "g"}})
                    .Create();
            return await _client.SubmitAsync<Traverser>(requestMsg).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}