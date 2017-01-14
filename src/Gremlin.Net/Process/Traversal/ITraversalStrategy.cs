namespace Gremlin.Net.Process.Traversal
{
    public interface ITraversalStrategy
    {
        void Apply(Traversal traversal);
    }
}