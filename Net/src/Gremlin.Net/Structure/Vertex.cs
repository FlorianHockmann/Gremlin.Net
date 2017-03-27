namespace Gremlin.Net.Structure
{
    /// <summary>
    ///     Represents a vertex.
    /// </summary>
    public class Vertex : Element
    {
        /// <summary>
        ///     The default label to use for a vertex.
        /// </summary>
        public const string DefaultLabel = "vertex";

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vertex" /> class.
        /// </summary>
        /// <param name="id">The id of the vertex.</param>
        /// <param name="label">The label of the vertex.</param>
        public Vertex(object id, string label = DefaultLabel)
            : base(id, label)
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"v[{Id}]";
        }
    }
}