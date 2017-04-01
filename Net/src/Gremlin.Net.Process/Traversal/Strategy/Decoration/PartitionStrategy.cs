using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    /// <summary>
    ///     Partitions the vertices, edges and vertex properties of a graph into String named partitions.
    /// </summary>
    public class PartitionStrategy : AbstractTraversalStrategy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PartitionStrategy" /> class.
        /// </summary>
        /// <param name="partitionKey">Specifies the partition key name.</param>
        /// <param name="writePartition">
        ///     Specifies the name of the partition to write when adding vertices, edges and vertex
        ///     properties.
        /// </param>
        /// <param name="readPartitions">Specifies the partition of the graph to read from.</param>
        /// <param name="includeMetaProperties">Set to true if vertex properties should get assigned to partitions.</param>
        public PartitionStrategy(string partitionKey = null, string writePartition = null,
            IEnumerable<string> readPartitions = null, bool? includeMetaProperties = null)
        {
            if (partitionKey != null)
                Configuration["partitionKey"] = partitionKey;
            if (writePartition != null)
                Configuration["writePartition"] = writePartition;
            if (readPartitions != null)
                Configuration["readPartitions"] = readPartitions;
            if (includeMetaProperties != null)
                Configuration["includeMetaProperties"] = includeMetaProperties.Value;
        }
    }
}