using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Process.Remote;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    /// <summary>
    ///     A <see cref="IRemoteConnection" /> implementation for Gremlin Server.
    /// </summary>
    public class DriverRemoteConnection : IRemoteConnection, IDisposable
    {
        private readonly IGremlinClient _client;

        /// <summary>
        ///     Initializes a new <see cref="IRemoteConnection" />.
        /// </summary>
        /// <param name="client">The <see cref="IGremlinClient" /> that will be used for the connection.</param>
        /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
        public DriverRemoteConnection(IGremlinClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        ///     Submits <see cref="Bytecode" /> for evaluation to a remote Gremlin Server.
        /// </summary>
        /// <param name="bytecode">The <see cref="Bytecode" /> to submit.</param>
        /// <returns>A <see cref="Traversal" /> allowing to access the results and side-effects.</returns>
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

        /// <inheritdoc />
        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}