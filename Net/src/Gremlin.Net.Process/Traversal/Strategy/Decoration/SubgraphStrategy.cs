namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    /// <summary>
    ///     Provides a way to limit the view of a <see cref="Traversal" />.
    /// </summary>
    public class SubgraphStrategy : AbstractTraversalStrategy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SubgraphStrategy" /> class.
        /// </summary>
        /// <param name="vertexCriterion">Constrains vertices for the <see cref="Traversal" />.</param>
        /// <param name="edgeCriterion">Constrains edges for the <see cref="Traversal" />.</param>
        /// <param name="vertexPropertyCriterion">Constrains vertex properties for the <see cref="Traversal" />.</param>
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