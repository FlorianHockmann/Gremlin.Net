using System.Threading.Tasks;

namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     A <see cref="ITraversalStrategy"/> defines a particular atomic operation for mutating a
    ///     <see cref="Traversal.Traversal"/> prior to its evaluation.
    /// </summary>
    public interface ITraversalStrategy
    {
        /// <summary>
        ///     Applies the strategy to the given <see cref="Traversal.Traversal"/>.
        /// </summary>
        /// <param name="traversal">The <see cref="Traversal.Traversal"/> the strategy should be applied to.</param>
        void Apply(Traversal traversal);

        /// <summary>
        ///     Applies the strategy to the given <see cref="Traversal.Traversal"/> asynchronously.
        /// </summary>
        /// <param name="traversal">The <see cref="Traversal.Traversal"/> the strategy should be applied to.</param>
        Task ApplyAsync(Traversal traversal);
    }
}