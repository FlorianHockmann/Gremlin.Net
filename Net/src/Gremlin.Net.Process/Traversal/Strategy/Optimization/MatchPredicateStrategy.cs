namespace Gremlin.Net.Process.Traversal.Strategy.Optimization
{
    /// <summary>
    ///     Folds any post<c>Where()</c> step that maintains a traversal constraint into <c>Match()</c>.
    /// </summary>
    public class MatchPredicateStrategy : AbstractTraversalStrategy
    {
    }
}