using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    public class BytecodeGraphSONSerializerTests
    {
        [Fact]
        public void Serialize_g_V()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");
            var graphsonWriter = CreateGraphSONWriter();

            var graphSON = graphsonWriter.WriteObject(bytecode);

            Assert.Equal("{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"]]}}", graphSON);
        }

        [Fact]
        public void Serialize_g_V_Count()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");
            bytecode.AddStep("count");
            var graphsonWriter = CreateGraphSONWriter();

            var graphSON = graphsonWriter.WriteObject(bytecode);

            var expectedGraphSon = "{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"],[\"count\"]]}}";
            Assert.Equal(expectedGraphSon, graphSON);
        }

        [Fact]
        public void Serialize_g_V_HasXPerson_Name_GremlinX_Count()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");
            bytecode.AddStep("has", "Person", "Name", "Gremlin");
            bytecode.AddStep("count");
            var graphsonWriter = CreateGraphSONWriter();

            var graphSON = graphsonWriter.WriteObject(bytecode);

            var expectedGraphSon =
                "{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"],[\"has\",\"Person\",\"Name\",\"Gremlin\"],[\"count\"]]}}";
            Assert.Equal(expectedGraphSon, graphSON);
        }

        [Fact]
        public void NumberSerializationTest()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V", (long)1);
            bytecode.AddStep("has", "age", 20);
            bytecode.AddStep("has", "height", 6.5);
            var graphsonWriter = CreateGraphSONWriter();

            var graphSON = graphsonWriter.WriteObject(bytecode);

            var expectedGraphSon = "{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\",{\"@type\":\"g:Int64\",\"@value\":1}],[\"has\",\"age\",{\"@type\":\"g:Int32\",\"@value\":20}],[\"has\",\"height\",{\"@type\":\"g:Double\",\"@value\":6.5}]]}}";
            Assert.Equal(expectedGraphSon, graphSON);
        }

        [Fact]
        public void SimpleDeserializeTest()
        {
            dynamic d = JObject.Parse("{\"@type\":\"g:Traverser\",\"@value\":1}");

            Assert.NotNull(d);
            Assert.Equal("g:Traverser", (string)d["@type"]);
        }

        [Fact]
        public void NestedTraversalSerializationTest()
        {
            var bytecode = new Bytecode();
            bytecode.AddStep("V");
            var nestedBytecode = new Bytecode();
            var nestedTraversal = new TestTraversal(nestedBytecode);
            nestedBytecode.AddStep("out");
            bytecode.AddStep("repeat", nestedTraversal);
            var graphsonWriter = CreateGraphSONWriter();

            var graphSON = graphsonWriter.WriteObject(bytecode);

            var expectedGraphSon =
                "{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"],[\"repeat\",{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"out\"]]}}]]}}";
            Assert.Equal(expectedGraphSon, graphSON);
        }

        private GraphSONWriter CreateGraphSONWriter()
        {
            return new GraphSONWriter();
        }
    }

    internal class TestTraversal : Traversal
    {
        public TestTraversal(Bytecode bytecode)
        {
            Bytecode = bytecode;
        }
    }
}