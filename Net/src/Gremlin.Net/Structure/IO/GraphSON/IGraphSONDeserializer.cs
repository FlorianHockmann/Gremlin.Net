using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    /// <summary>
    ///     Supports deserializing GraphSON into an object.
    /// </summary>
    public interface IGraphSONDeserializer
    {
        /// <summary>
        ///     Deserializes GraphSON to an object.
        /// </summary>
        /// <param name="graphsonObject">The GraphSON object to objectify.</param>
        /// <param name="reader">A <see cref="GraphSONReader" /> that can be used to objectify properties of the GraphSON object.</param>
        /// <returns>The deserialized object.</returns>
        dynamic Objectify(JToken graphsonObject, GraphSONReader reader);
    }
}