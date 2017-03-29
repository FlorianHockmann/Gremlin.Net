namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    /// <summary>
    ///     ConnectiveStrategy rewrites the binary conjunction form of <c>a.and().b</c> into an AndStep of
    ///     <c>and(a, b)</c> (likewise for OrStep).
    /// </summary>
    public class ConnectiveStrategy : AbstractTraversalStrategy
    {
    }
}