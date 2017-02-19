using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal.Strategy.Decoration
{
    public class PartitionStrategy : AbstractTraversalStrategy
    {
        public PartitionStrategy(string partitionKey = null, string writePartition = null,
            IEnumerable<string> readPartitions = null, bool? includeMetaProperties = null)
        {
            if (partitionKey != null)
            {
                Configuration["partitionKey"] = partitionKey;
            }
            if (writePartition != null)
            {
                Configuration["writePartition"] = writePartition;
            }
            if (readPartitions != null)
            {
                Configuration["readPartitions"] = readPartitions;
            }
            if (includeMetaProperties != null)
            {
                Configuration["includeMetaProperties"] = includeMetaProperties.Value;
            }
        }
    }
}