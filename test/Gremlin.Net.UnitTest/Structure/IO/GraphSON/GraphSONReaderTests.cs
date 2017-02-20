using System;
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

        [Fact]
        public void EdgeReader_EdgeWithProperties_DeserializeToEdge()
        {
            var graphSon =
                "{\"@type\":\"g:Edge\", \"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":17},\"label\":\"knows\",\"inV\":\"x\",\"outV\":\"y\",\"inVLabel\":\"xLab\",\"properties\":{\"aKey\":\"aValue\",\"bKey\":true}}}";
            var reader = CreateStandardGraphSONReader();

            Edge readEdge = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal((long)17, readEdge.Id);
            Assert.Equal("knows", readEdge.Label);
            Assert.Equal(new Vertex("x", "xLabel"), readEdge.InV);
            Assert.Equal(new Vertex("y"), readEdge.OutV);
        }

        [Fact]
        public void PropertyReader_PropertyWithEdgeElement_DeserializeToProperty()
        {
            var graphSon =
                "{\"@type\":\"g:Property\",\"@value\":{\"key\":\"aKey\",\"value\":{\"@type\":\"g:Int64\",\"@value\":17},\"element\":{\"@type\":\"g:Edge\",\"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":122},\"label\":\"knows\",\"inV\":\"x\",\"outV\":\"y\",\"inVLabel\":\"xLab\"}}}}";
            var reader = CreateStandardGraphSONReader();

            Property readProperty = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal("aKey", readProperty.Key);
            Assert.Equal((long)17, readProperty.Value);
            Assert.Equal(typeof(Edge), readProperty.Element.GetType());
            var edge = readProperty.Element as Edge;
            Assert.Equal((long)122, edge.Id);
            Assert.Equal("knows", edge.Label);
            Assert.Equal("x", edge.InV.Id);
            Assert.Equal("y", edge.OutV.Id);
        }

        [Fact]
        public void VertexPropertyReader_SimpleVertexProperty_DeserializeToVertexProperty()
        {
            var graphSon =
                "{\"@type\":\"g:VertexProperty\",\"@value\":{\"id\":\"anId\",\"label\":\"aKey\",\"value\":true,\"vertex\":{\"@type\":\"g:Int32\",\"@value\":9}}}";
            var reader = CreateStandardGraphSONReader();

            VertexProperty readVertexProperty = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal("anId", readVertexProperty.Id);
            Assert.Equal("aKey", readVertexProperty.Label);
            Assert.True(readVertexProperty.Value);
            Assert.NotNull(readVertexProperty.Vertex);
        }

        [Fact]
        public void PathReader_PathWithVerticesAndString_DeserializeToPath()
        {
            var graphSon =
                "{\"@type\":\"g:Path\",\"@value\":{\"labels\":[[\"a\"],[\"b\",\"c\"],[]],\"objects\":[{\"@type\":\"g:Vertex\",\"@value\":{\"id\":{\"@type\":\"g:Int32\",\"@value\":1},\"label\":\"person\",\"properties\":{\"name\":[{\"@type\":\"g:VertexProperty\",\"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":0},\"value\":\"marko\",\"label\":\"name\"}}],\"age\":[{\"@type\":\"g:VertexProperty\",\"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":1},\"value\":{\"@type\":\"g:Int32\",\"@value\":29},\"label\":\"age\"}}]}}},{\"@type\":\"g:Vertex\",\"@value\":{\"id\":{\"@type\":\"g:Int32\",\"@value\":3},\"label\":\"software\",\"properties\":{\"name\":[{\"@type\":\"g:VertexProperty\",\"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":4},\"value\":\"lop\",\"label\":\"name\"}}],\"lang\":[{\"@type\":\"g:VertexProperty\",\"@value\":{\"id\":{\"@type\":\"g:Int64\",\"@value\":5},\"value\":\"java\",\"label\":\"lang\"}}]}}},\"lop\"]}}";
            var reader = CreateStandardGraphSONReader();

            Path readPath = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal("[v[1], v[3], lop]", readPath.ToString());
            Assert.Equal(new Vertex(1), readPath[0]);
            Assert.Equal(new Vertex(1), readPath["a"]);
            Assert.Equal("lop", readPath[2]);
            Assert.Equal(3, readPath.Count);
        }

        [Fact]
        public void UuidReader_Uuid_DeserializeToGuid()
        {
            var graphSon = "{\"@type\":\"g:UUID\",\"@value\":\"41d2e28a-20a4-4ab0-b379-d810dede3786\"}";
            var reader = CreateStandardGraphSONReader();

            Guid readGuid = reader.ToObject(JObject.Parse(graphSon));

            Assert.Equal(Guid.Parse("41d2e28a-20a4-4ab0-b379-d810dede3786"), readGuid);
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