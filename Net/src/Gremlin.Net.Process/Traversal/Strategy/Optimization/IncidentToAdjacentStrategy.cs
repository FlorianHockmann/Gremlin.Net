namespace Gremlin.Net.Process.Traversal.Strategy.Optimization
{
    /// <summary>
    ///     Replaces <c>.OutE().InV()</c> with <c>.Out()</c>, <c>.InE().OutV()</c> with <c>In()</c> and <c>.BothE().BothV()</c>
    ///     with <c>Both()</c>.
    /// </summary>
    public class IncidentToAdjacentStrategy : AbstractTraversalStrategy
    {
    }
}