using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    internal class DriverRemoteTraversalSideEffects : ITraversalSideEffects
    {
        private readonly IGremlinClient _gremlinClient;
        private readonly List<string> _keys = new List<string>();
        private readonly Guid _serverSideEffectId;
        private readonly Dictionary<string, object> _sideEffects = new Dictionary<string, object>();
        private bool _closed;
        private bool _retrievedAllKeys;

        public DriverRemoteTraversalSideEffects(IGremlinClient gremlinClient, Guid serverSideEffectId)
        {
            _gremlinClient = gremlinClient;
            _serverSideEffectId = serverSideEffectId;
        }

        public void Dispose()
        {
            Close();
        }

        public IReadOnlyCollection<string> Keys()
        {
            if (_closed && !_retrievedAllKeys)
                throw new InvalidOperationException("Traversal has been closed - side-effect keys cannot be retrieved");
            if (!_retrievedAllKeys)
            {
                _keys.AddRange(RetrieveKeys());
                _retrievedAllKeys = true;
            }
            return _keys;
        }

        private IEnumerable<string> RetrieveKeys()
        {
            return _gremlinClient.SubmitAsync<string>(SideEffectKeysMessage()).Result;
        }

        private RequestMessage SideEffectKeysMessage()
        {
            return RequestMessage.Build(Tokens.OpsKeys)
                .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                .Processor(Tokens.ProcessorTraversal)
                .Create();
        }

        public object Get(string key)
        {
            if (!Keys().Contains(key))
                throw new KeyNotFoundException($"Side effect key {key} does not exist");
            if (!_sideEffects.ContainsKey(key))
            {
                if (_closed)
                    throw new InvalidOperationException(
                        "Traversal has been closed - no new side-effects can be retrieved");
                _sideEffects.Add(key, RetrieveSideEffectsForKey(key));
            }
            return _sideEffects[key];
        }

        private object RetrieveSideEffectsForKey(string key)
        {
            return _gremlinClient.SubmitWithSingleResultAsync<object>(SideEffectGatherMessage(key)).Result;
        }

        private RequestMessage SideEffectGatherMessage(string key)
        {
            return RequestMessage.Build(Tokens.OpsGather)
                .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                .AddArgument(Tokens.ArgsSideEffectKey, key)
                .AddArgument(Tokens.ArgsAliases, new Dictionary<string, string> { { "g", "g" } })
                .Processor(Tokens.ProcessorTraversal)
                .Create();
        }

        public void Close()
        {
            if (_closed) return;
            CloseSideEffects();
            _closed = true;
        }

        private void CloseSideEffects()
        {
            _gremlinClient.SubmitAsync<object>(SideEffectCloseMessage()).Wait();
        }

        private RequestMessage SideEffectCloseMessage()
        {
            return RequestMessage.Build(Tokens.OpsClose)
                .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                .Processor(Tokens.ProcessorTraversal)
                .Create();
        }
    }
}