using System;
using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    public class BytecodeGraphSONSerializerTests
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new BytecodeConverter(),
                new Int32Converter(),
                new Int64Converter(),
                new FloatConverter(),
                new DoubleConverter()
            }
        };

        [Fact]
        public void Serialize_g_V()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");

            var graphSON = JsonConvert.SerializeObject(bytecode, _serializerSettings);

            Assert.Equal("{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\"]]}}", graphSON);
        }

        [Fact]
        public void Serialize_g_V_Count()
        {
            var bytecode = new Bytecode();
            bytecode.AddSource("V");
            bytecode.AddStep("count");

            var graphSON = JsonConvert.SerializeObject(bytecode, _serializerSettings);

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

            var graphSON = JsonConvert.SerializeObject(bytecode, _serializerSettings);

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

            var graphSON = JsonConvert.SerializeObject(bytecode, _serializerSettings);

            var expectedGraphSon = "{\"@type\":\"g:Bytecode\",\"@value\":{\"step\":[[\"V\",{\"@type\":\"g:Int64\",\"@value\":1}],[\"has\",\"age\",{\"@type\":\"g:Int32\",\"@value\":20}],[\"has\",\"height\",{\"@type\":\"g:Double\",\"@value\":6.5}]]}}";
            Assert.Equal(expectedGraphSon, graphSON);
        }
    }
}