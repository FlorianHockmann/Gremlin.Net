using System;
using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal
{
    public interface ITraversalSideEffects : IDisposable
    {
        IReadOnlyCollection<string> Keys();
        object Get(string key);
        void Close();
    }
}