namespace Gremlin.Net.Structure.IO
{
    public class Edge : Element
    {
        public Edge(object id, Vertex outV, string label, Vertex inV)
            : base(id, label)
        {
            OutV = outV;
            InV = inV;
        }

        public Vertex InV { get; set; }

        public Vertex OutV { get; set; }

        public override string ToString()
        {
            return $"e[{Id}][{OutV.Id}-{Label}->{InV.Id}]";
        }
    }
}