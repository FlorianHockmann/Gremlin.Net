namespace Gremlin.Net.Structure
{
    public class VertexProperty : Element
    {
        public VertexProperty(object id, string label, dynamic value, Vertex vertex)
            : base(id, label)
        {
            Value = value;
            Vertex = vertex;
        }

        public dynamic Value { get; }
        public Vertex Vertex { get; }
        public string Key => Label;

        public override string ToString()
        {
            return $"vp[{Label}->{Value}]";
        }
    }
}