namespace Gremlin.Net.Structure
{
    /// <summary>
    ///     Represents an edge between to vertices.
    /// </summary>
    public class Edge : Element
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Edge" /> class.
        /// </summary>
        /// <param name="id">The id of the edge.</param>
        /// <param name="outV">The outgoing/tail vertex of the edge.</param>
        /// <param name="label">The label of the edge.</param>
        /// <param name="inV">The incoming/head vertex of the edge.</param>
        public Edge(object id, Vertex outV, string label, Vertex inV)
            : base(id, label)
        {
            OutV = outV;
            InV = inV;
        }

        /// <summary>
        ///     Gets or sets the incoming/head vertex of this edge.
        /// </summary>
        public Vertex InV { get; set; }

        /// <summary>
        ///     Gets or sets the outgoing/tail vertex of this edge.
        /// </summary>
        public Vertex OutV { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"e[{Id}][{OutV.Id}-{Label}->{InV.Id}]";
        }
    }
}