using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;

namespace Gremlin.Net.Driver.Messages.Traversal
{
    public class BytecodeArguments
    {
        [JsonProperty(PropertyName = "gremlin")]
        public Bytecode Bytecode { get; set; }

        /// <summary>
        /// Gets or sets aliases that allow to bind names of Graph and TraversalSource to other names.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey,TValue}"/> that allows globally bound Graph and TraversalSource objects to be aliased to different variable names for purposes of the current request. The value represents the name of the global variable and its key represents the new binding name as it will be referenced in the Gremlin query. For example, if the Gremlin Server defines two TraversalSource instances named g1 and g2, it would be possible to send an alias pair with key of "g" and value of "g2" and thus allow the script to refer to "g2" simply as "g".</value>
        [JsonProperty(PropertyName = "aliases", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Aliases { get; set; } = new Dictionary<string, string> { {"g", "g"} };
    }
}