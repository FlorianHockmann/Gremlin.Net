namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    public class SubgraphStrategy : AbstractTraversalStrategy
    {
        public SubgraphStrategy(Traversal vertexCriterion = null, Traversal edgeCriterion = null,
            Traversal vertexPropertyCriterion = null)
        {
            if (vertexCriterion != null)
                Configuration["vertices"] = vertexCriterion;
            if (edgeCriterion != null)
                Configuration["edges"] = edgeCriterion;
            if (vertexPropertyCriterion != null)
                Configuration["vertexProperties"] = vertexPropertyCriterion;
        }
    }
}