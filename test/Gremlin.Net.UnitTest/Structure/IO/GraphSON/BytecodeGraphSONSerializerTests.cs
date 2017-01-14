using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
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

            var graphSON = JsonConvert.SerializeObject(bytecode);

            Assert.Equal("{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"]]}}", graphSON);
        }

        [Fact]
        public void Serialize_g_V_Count()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");
            bytecode.AddStep("count");

            var graphSON = JsonConvert.SerializeObject(bytecode);

            Assert.Equal("{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"],[\"count\"]]}}", graphSON);
        }
    }
}