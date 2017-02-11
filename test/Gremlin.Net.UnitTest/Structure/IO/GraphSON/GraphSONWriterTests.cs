using System.Collections.Generic;
using Gremlin.Net.Structure.IO.GraphSON;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure.IO.GraphSON
{
    public class GraphSONWriterTests
    {
        [Fact]
        public void ArraySerializationTest()
        {
            var writer = CreateStandardGraphSONWriter();
            var array = new[] {5,6};

            var serializedGraphSON = writer.WriteObject(array);

            var expectedGraphSON = "[{\"@type\":\"g:Int32\",\"@value\":5},{\"@type\":\"g:Int32\",\"@value\":6}]";
            Assert.Equal(expectedGraphSON, serializedGraphSON);
        }

        [Fact]
        public void ListSerializationTest()
        {
            var writer = CreateStandardGraphSONWriter();
            var list = new List<int> {5, 6};

            var serializedGraphSON = writer.WriteObject(list.ToArray());

            var expectedGraphSON = "[{\"@type\":\"g:Int32\",\"@value\":5},{\"@type\":\"g:Int32\",\"@value\":6}]";
            Assert.Equal(expectedGraphSON, serializedGraphSON);
        }

        [Fact]
        public void DictionarySerializationTest()
        {
            var writer = CreateStandardGraphSONWriter();
            var dictionary = new Dictionary<string, dynamic>
            {
                {"age", new List<int> {29}},
                {"name", new List<string> {"marko"}}
            };

            var serializedDict = writer.WriteObject(dictionary);

            var expectedGraphSON = "{\"age\":[{\"@type\":\"g:Int32\",\"@value\":29}],\"name\":[\"marko\"]}";
            Assert.Equal(expectedGraphSON, serializedDict);

        }

        [Fact]
        public void EnumSerializationTest()
        {
            var writer = CreateStandardGraphSONWriter();

            var serializedEnum = writer.WriteObject(T.label);

            var expectedGraphSON = "{\"@type\":\"g:T\",\"@value\":\"label\"}";
            Assert.Equal(expectedGraphSON, serializedEnum);
        }

        private GraphSONWriter CreateStandardGraphSONWriter()
        {
            return new GraphSONWriter();
        }
    }

    internal enum T
    {
        label
    }
}