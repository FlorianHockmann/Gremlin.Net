namespace Gremlin.Net.Process.Traversal.Strategy.Optimization
{
    /// <summary>
    ///     Inserts <c>Barrier()</c>-steps into a <see cref="Traversal" /> where appropriate in order to gain the "bulking
    ///     optimization".
    /// </summary>
    public class LazyBarrierStrategy : AbstractTraversalStrategy
    {
    }
}