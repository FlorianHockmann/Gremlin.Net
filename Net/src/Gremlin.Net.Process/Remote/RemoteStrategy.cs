using System.Threading.Tasks;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Process.Remote
{
    /// <summary>
    ///     Reconstructs a <see cref="Traversal.Traversal" /> by submitting it to a remote server via an
    ///     <see cref="IRemoteConnection" /> instance.
    /// </summary>
    public class RemoteStrategy : ITraversalStrategy
    {
        private readonly IRemoteConnection _remoteConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteStrategy" /> class.
        /// </summary>
        /// <param name="remoteConnection">The <see cref="IRemoteConnection" /> that should be used.</param>
        public RemoteStrategy(IRemoteConnection remoteConnection)
        {
            _remoteConnection = remoteConnection;
        }

        /// <inheritdoc />
        public void Apply(Traversal.Traversal traversal)
        {
            ApplyAsync(traversal).Wait();
        }

        /// <inheritdoc />
        public async Task ApplyAsync(Traversal.Traversal traversal)
        {
            if (traversal.Traversers != null) return;
            var remoteTraversal = await _remoteConnection.SubmitAsync(traversal.Bytecode).ConfigureAwait(false);
            traversal.SideEffects = remoteTraversal.SideEffects;
            traversal.Traversers = remoteTraversal.Traversers;
        }
    }
}