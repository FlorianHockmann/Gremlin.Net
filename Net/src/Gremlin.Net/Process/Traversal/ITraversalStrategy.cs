using System.Threading.Tasks;

namespace Gremlin.Net.Process.Traversal
{
    public interface ITraversalStrategy
    {
        void Apply(Traversal traversal);
        Task ApplyAsync(Traversal traversal);
    }
}