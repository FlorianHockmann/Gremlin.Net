using System;
using Newtonsoft.Json;

namespace Gremlin.Net.Driver.Messages
{
    public abstract class RequestMessage
    {
        /// <summary>
        /// Gets or sets the ID of this request message.
        /// </summary>
        /// <value>A UUID representing the unique identification for the request.</value>
        [JsonProperty(PropertyName = "requestId")]
        public Guid RequestId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the operation that should be executed by the Gremlin Server.
        /// </summary>
        /// <value>The name of the "operation" to execute based on the available OpProcessor configured in the Gremlin Server. This defaults to "eval" which evaluates a request script.</value>
        [JsonProperty(PropertyName = "op")]
        public abstract string Operation { get; }

        /// <summary>
        /// Gets or sets the name of the OpProcessor to utilize.
        /// </summary>
        /// <value>The name of the OpProcessor to utilize. This defaults to an empty string which represents the default OpProcessor for evaluating scripts.</value>
        [JsonProperty(PropertyName = "processor")]
        public abstract string Processor { get; }
    }
}