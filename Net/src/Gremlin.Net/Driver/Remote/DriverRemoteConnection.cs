using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Messages;
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
    }
}