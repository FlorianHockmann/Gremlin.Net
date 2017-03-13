using Gremlin.CSharp.Process;
using Gremlin.Net.Structure.IO.GraphSON;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class GraphSONWriterTests
    {
        [Fact]
        public void LongPredicateSerializationTest()
        {
            var writer = CreateStandardGraphSONWriter();
            var predicate = P.Lt("b").Or(P.Gt("c")).And(P.Neq("d"));

            var graphSon = writer.WriteObject(predicate);

            const string expected =
                "{\"@type\":\"g:P\",\"@value\":{\"predicate\":\"and\",\"value\":[{\"@type\":\"g:P\",\"@value\":{\"predicate\":\"or\",\"value\":[{\"@type\":\"g:P\",\"@value\":{\"predicate\":\"lt\",\"value\":\"b\"}},{\"@type\":\"g:P\",\"@value\":{\"predicate\":\"gt\",\"value\":\"c\"}}]}},{\"@type\":\"g:P\",\"@value\":{\"predicate\":\"neq\",\"value\":\"d\"}}]}}";
            Assert.Equal(expected, graphSon);
        }

        private GraphSONWriter CreateStandardGraphSONWriter()
        {
            return new GraphSONWriter();
        }
    }
}