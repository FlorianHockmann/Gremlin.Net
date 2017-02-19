namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    public class HaltedTraverserStrategy : AbstractTraversalStrategy
    {
        public HaltedTraverserStrategy(string haltedTraverserFactoryName = null)
        {
            if (haltedTraverserFactoryName != null)
            {
                Configuration["haltedTraverserFactory"] = haltedTraverserFactoryName;
            }
        }
    }
}