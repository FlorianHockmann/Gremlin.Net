using System;
using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     A <see cref="Traversal" /> can maintain global sideEffects.
    /// </summary>
    public interface ITraversalSideEffects : IDisposable
    {
        /// <summary>
        ///     Retrieves the keys of the side-effect that can be supplied to <see cref="Get" />.
        /// </summary>
        /// <returns>The keys of the side-effect.</returns>
        IReadOnlyCollection<string> Keys();

        /// <summary>
        ///     Gets the side-effect associated with the provided key.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <returns>The value associated with key.</returns>
        object Get(string key);

        /// <summary>
        ///     Invalidates the side effect cache for traversal.
        /// </summary>
        void Close();
    }
}