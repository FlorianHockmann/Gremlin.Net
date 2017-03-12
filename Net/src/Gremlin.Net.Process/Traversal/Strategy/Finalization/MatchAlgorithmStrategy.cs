namespace Gremlin.Net.Process.Traversal.Strategy.Finalization
{
    public class MatchAlgorithmStrategy : AbstractTraversalStrategy
    {
        public MatchAlgorithmStrategy(string matchAlgorithm = null)
        {
            if (matchAlgorithm != null)
                Configuration["matchAlgorithm"] = matchAlgorithm;
        }
    }
}