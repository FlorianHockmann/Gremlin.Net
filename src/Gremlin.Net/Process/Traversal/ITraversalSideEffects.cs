using System;
using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal
{
    public interface ITraversalSideEffects : IDisposable
    {
        IEnumerable<string> Keys();
        TValue Get<TValue>(string key);
    }
}