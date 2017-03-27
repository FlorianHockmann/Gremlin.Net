namespace Gremlin.Net.Structure
{
    /// <summary>
    ///     A <see cref="VertexProperty" /> denotes a key/value pair associated with a <see cref="Vertex" />.
    /// </summary>
    public class VertexProperty : Element
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VertexProperty" /> class.
        /// </summary>
        /// <param name="id">The id of the vertex property.</param>
        /// <param name="label">The label of the vertex property.</param>
        /// <param name="value">The id of the vertex property.</param>
        /// <param name="vertex">The <see cref="Vertex" /> that owns this <see cref="VertexProperty" />.</param>
        public VertexProperty(object id, string label, dynamic value, Vertex vertex)
            : base(id, label)
        {
            Value = value;
            Vertex = vertex;
        }

        /// <summary>
        ///     The value of this <see cref="VertexProperty" />.
        /// </summary>
        public dynamic Value { get; }

        /// <summary>
        ///     The <see cref="Vertex" /> that owns this <see cref="VertexProperty" />.
        /// </summary>
        public Vertex Vertex { get; }

        /// <summary>
        ///     The key of this <see cref="VertexProperty" />.
        /// </summary>
        public string Key => Label;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"vp[{Label}->{Value}]";
        }
    }
}