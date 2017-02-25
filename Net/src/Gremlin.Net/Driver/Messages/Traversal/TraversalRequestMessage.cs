using Newtonsoft.Json;

namespace Gremlin.Net.Driver.Messages.Traversal
{
    public abstract class TraversalRequestMessage<TArguments> : RequestMessage
    {
        public override string Processor => "traversal";

        /// <summary>
        /// Gets or sets parameters for this <see cref="TraversalRequestMessage"/> to pass to Gremlin Server.
        /// </summary>
        [JsonProperty(PropertyName = "args")]
        public TArguments Arguments { get; set; }
    }
}