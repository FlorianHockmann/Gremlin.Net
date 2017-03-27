using System.Collections.Generic;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    /// <summary>
    ///     Supports serializing of an object to GraphSON.
    /// </summary>
    public interface IGraphSONSerializer
    {
        /// <summary>
        ///     Transforms an object into a dictionary that resembles its GraphSON representation.
        /// </summary>
        /// <param name="objectData">The object to dictify.</param>
        /// <param name="writer">A <see cref="GraphSONWriter" /> that can be used to dictify properties of the object.</param>
        /// <returns>The GraphSON representation.</returns>
        Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer);
    }
}