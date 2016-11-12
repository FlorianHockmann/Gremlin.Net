using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Messages;

namespace Gremlin.Net
{
    public interface IGremlinClient : IDisposable
    {
        Task<IList<T>> SubmitAsync<T>(ScriptRequestMessage requestMessage);
    }
}