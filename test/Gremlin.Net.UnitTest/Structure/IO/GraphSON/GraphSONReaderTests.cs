using System.Collections.Generic;
using Gremlin.Net.Structure;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    public class GraphSONReaderTests
    {
        [Fact]
        public void Int32DeserializationTest()
        {
            var serializedValue = "{\"@type\":\"g:Int32\",\"@value\":5}";
            var reader = CreateStandardGraphSONReader();

            var jObject = JObject.Parse(serializedValue);
            var deserializedValue = reader.ToObject(jObject);

            Assert.Equal(5, deserializedValue);
        }

        [Fact]
        public void ListDeserializationTest()
        {
            var serializedValue = "[{\"@type\":\"g:Int32\",\"@value\":5},{\"@type\":\"g:Int32\",\"@value\":6}]";
            var reader = CreateStandardGraphSONReader();

            var jObject = JArray.Parse(serializedValue);
            var deserializedValue = reader.ToObject(jObject);

            Assert.Equal(new List<object> {5, 6}, deserializedValue);
        }

        [Fact]
        public void DictionaryDeserializationTest()
        {
            var serializedDict = "{\"age\":[{\"@type\":\"g:Int32\",\"@value\":29}],\"name\":[\"marko\"]}";
            var reader = CreateStandardGraphSONReader();

            var jObject = JObject.Parse(serializedDict);
            var deserializedDict = reader.ToObject(jObject);

            var expectedDict = new Dictionary<string, dynamic>
            {
                {"age", new List<object> {29}},
                {"name", new List<object> {"marko"}}
            };
            Assert.Equal(expectedDict, deserializedDict);
        }

        [Fact]
        public void CustomDeserializationTest()
        {
            var deserializerByGraphSONType = new Dictionary<string, IGraphSONDeserializer>
            {
                {"NS:TestClass", new TestGraphSONDeserializer()}
            };
            var reader = new GraphSONReader(deserializerByGraphSONType);
            var graphSON = "{\"@type\":\"NS:TestClass\",\"@value\":\"test\"}";

            var jObject = JObject.Parse(graphSON);
            var readObj = reader.ToObject(jObject);

            Assert.Equal("test", readObj.Value);
        }

        [Fact]
        public void VertexReader_ReadSimpleVertexGraphSon_DeserializeToVertex()
        {
            var graphSon = "{\"@type\":\"g:Vertex\", \"@value\":{\"id\":{\"@type\":\"g:Float\",\"@value\":45.23}}}";
            var reader = CreateStandardGraphSONReader();

            var readVertex = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal(new Vertex(45.23f), readVertex);
        }

        [Fact]
        public void VertexReader_ReadVertexWithEdges_DeserializeToVertex()
        {
            var graphSon = "{\"@type\":\"g:Vertex\", \"@value\":{\"id\":{\"@type\":\"g:Int32\",\"@value\":1},\"label\":\"person\",\"outE\":{\"created\":[{\"id\":{\"@type\":\"g:Int32\",\"@value\":9},\"inV\":{\"@type\":\"g:Int32\",\"@value\":3},\"properties\":{\"weight\":{\"@type\":\"g:Double\",\"@value\":0.4}}}],\"knows\":[{\"id\":{\"@type\":\"g:Int32\",\"@value\":7},\"inV\":{\"@type\":\"g:Int32\",\"@value\":2},\"properties\":{\"weight\":{\"@type\":\"g:Double\",\"@value\":0.5}}},{\"id\":{\"@type\":\"g:Int32\",\"@value\":8},\"inV\":{\"@type\":\"g:Int32\",\"@value\":4},\"properties\":{\"weight\":{\"@type\":\"g:Double\",\"@value\":1.0}}}]},\"properties\":{\"name\":[{\"id\":{\"@type\":\"g:Int64\",\"@value\":0},\"value\":\"marko\"}],\"age\":[{\"id\":{\"@type\":\"g:Int64\",\"@value\":1},\"value\":{\"@type\":\"g:Int32\",\"@value\":29}}]}}}";
            var reader = CreateStandardGraphSONReader();

            var readVertex = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal(new Vertex(1), readVertex);
            Assert.Equal("person", readVertex.Label);
            Assert.Equal(typeof(int), readVertex.Id.GetType());
        }

        private GraphSONReader CreateStandardGraphSONReader()
        {
            return new GraphSONReader();
        }
    }

    internal class TestGraphSONDeserializer : IGraphSONDeserializer
    {

        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            return new TestClass {Value = graphsonObject.ToString()};
        }
    }
}