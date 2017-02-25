namespace Gremlin.Net.Structure
{
    public class Vertex : Element
    {
        public const string DefaultLabel = "vertex";

        public Vertex(object id, string label = DefaultLabel)
            : base(id, label)
        {
        }

        public override string ToString()
        {
            return $"v[{Id}]";
        }
    }
}