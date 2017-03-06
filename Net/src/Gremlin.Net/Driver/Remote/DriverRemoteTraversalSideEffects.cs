using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Driver.Remote
{
    public class DriverRemoteTraversalSideEffects : ITraversalSideEffects
    {
        private readonly IGremlinClient _gremlinClient;
        private readonly Guid _serverSideEffectId;
        private readonly Dictionary<string, object> _sideEffects = new Dictionary<string, object>();
        private readonly List<string> _keys = new List<string>(); 
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
            {
                throw new InvalidOperationException("Traversal has been closed - side-effect keys cannot be retrieved");
            }
            if (!_retrievedAllKeys)
            {
                var msg =
                    RequestMessage.Build(Tokens.OpsKeys)
                        .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                        .Processor(Tokens.ProcessorTraversal)
                        .Create();
                _keys.AddRange(_gremlinClient.SubmitAsync<string>(msg).Result);
                _retrievedAllKeys = true;
            }
            return _keys;
        }

        public object Get(string key)
        {
            if (!Keys().Contains(key))
            {
                throw new KeyNotFoundException($"Side effect key {key} does not exist");
            }
            if (!_sideEffects.ContainsKey(key))
            {
                if (_closed)
                {
                    throw new InvalidOperationException("Traversal has been closed - no new side-effects can be retrieved");
                }
                var msg =
                    RequestMessage.Build(Tokens.OpsGather)
                        .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                        .AddArgument(Tokens.ArgsSideEffectKey, key)
                        .AddArgument(Tokens.ArgsAliases, new Dictionary<string, string> {{"g", "g"}})
                        .Processor(Tokens.ProcessorTraversal)
                        .Create();
                var sideEffects = _gremlinClient.SubmitWithSingleResultAsync<object>(msg).Result;
                _sideEffects.Add(key, sideEffects);
            }
            return _sideEffects[key];
        }

        public void Close()
        {
            if (!_closed)
            {
                var msg =
                    RequestMessage.Build(Tokens.OpsClose)
                        .AddArgument(Tokens.ArgsSideEffect, _serverSideEffectId)
                        .Processor(Tokens.ProcessorTraversal)
                        .Create();
                _gremlinClient.SubmitAsync<object>(msg).Wait();
                _closed = true;
            }
        }
    }
}