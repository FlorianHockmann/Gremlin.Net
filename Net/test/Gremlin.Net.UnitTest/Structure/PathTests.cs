using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Structure;
using Xunit;

namespace Gremlin.Net.UnitTest.Structure
{
    public class PathTests
    {
        [Fact]
        public void Constructor_ValidARguments_CorrectProperties()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};

            var path = new Path(labels, objects);

            Assert.Equal(labels, path.Labels);
            Assert.Equal(objects, path.Objects);
        }

        [Fact]
        public void ContainsKey_ExistingLabel_ReturnsTrue()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var path = new Path(labels, new List<object>());

            var containsKey = path.ContainsKey("c");

            Assert.True(containsKey);
        }

        [Fact]
        public void ContainsKey_NotExistingLabel_ReturnsFalse()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var path = new Path(labels, new List<object>());

            var containsKey = path.ContainsKey("z");

            Assert.False(containsKey);
        }

        [Fact]
        public void Count_VariousObjects_ReturnObjectCount()
        {
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(new List<List<string>>(), objects);

            var count = path.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public void Enumerator_SomeObject_EnumerateObjects()
        {
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(new List<List<string>>(), objects);

            var enumeratedObj = path.ToList();

            Assert.Equal(objects, enumeratedObj);
        }

        [Fact]
        public void Equals_EqualPath_ReturnTrue()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {1, new Vertex(1), "hello"});
            var secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {1, new Vertex(1), "hello"});

            var equals = firstPath.Equals(secondPath);

            Assert.True(equals);
        }

        [Fact]
        public void Equals_LabelsNotEqual_ReturnFalse()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {1, new Vertex(1), "hello"});
            var secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {1, new Vertex(1), "hello"});

            var equals = firstPath.Equals(secondPath);

            Assert.False(equals);
        }

        [Fact]
        public void Equals_ObjectsNotEqual_ReturnFalse()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {1, new Vertex(1), "hello"});
            var secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> {3, new Vertex(1), "hello"});

            var equals = firstPath.Equals(secondPath);

            Assert.False(equals);
        }

        [Fact]
        public void EqualsObject_EqualPath_ReturnTrue()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });
            object secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });

            var equals = firstPath.Equals(secondPath);

            Assert.True(equals);
        }

        [Fact]
        public void EqualsObject_LabelsNotEqual_ReturnFalse()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });
            object secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });

            var equals = firstPath.Equals(secondPath);

            Assert.False(equals);
        }

        [Fact]
        public void EqualsObject_ObjectsNotEqual_ReturnFalse()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });
            object secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 3, new Vertex(1), "hello" });

            var equals = firstPath.Equals(secondPath);

            Assert.False(equals);
        }

        [Fact]
        public void Equals_OtherIsNull_ReturnFalse()
        {
            var path = new Path(new List<List<string>> {new List<string> {"a", "b"},}, new List<object> {1});

            var equals = path.Equals(null);

            Assert.False(equals);
        }

        [Fact]
        public void GetHashCode_EqualPaths_EqualHashCodes()
        {
            var firstPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });
            var secondPath =
                new Path(
                    new List<List<string>>
                    {
                        new List<string> {"a", "b"},
                        new List<string> {"c", "b"},
                        new List<string>()
                    }, new List<object> { 1, new Vertex(1), "hello" });

            var firstHashCode = firstPath.GetHashCode();
            var secondHashCode = secondPath.GetHashCode();

            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Fact]
        public void IndexAccessor_InvalidIndex_Throw()
        {
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(new List<List<string>>(), objects);

            Assert.Throws<ArgumentOutOfRangeException>(() => path[3]);
        }

        [Fact]
        public void IndexAccessor_ValidIndices_ReturnObjects()
        {
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(new List<List<string>>(), objects);

            Assert.Equal(1, path[0]);
            Assert.Equal(new Vertex(1), path[1]);
            Assert.Equal("hello", path[2]);
        }

        [Fact]
        public void KeyAccessor_KeyWithMultipleObjects_ReturnObjects()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(labels, objects);

            var bObjects = path["b"];

            Assert.Equal(new List<object> {1, new Vertex(1)}, bObjects);
        }

        [Fact]
        public void KeyAccessor_OneObjectPerKey_ReturnCorrectObject()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(labels, objects);

            Assert.Equal(1, path["a"]);
            Assert.Equal(new Vertex(1), path["c"]);
            Assert.Equal(new Vertex(1), path["b"]);
        }

        [Fact]
        public void KeyAccessor_UnknownKey_Throw()
        {
            var path = new Path(new List<List<string>>(), new List<object>());

            Assert.Throws<KeyNotFoundException>(() => path["unknownKey"]);
        }

        [Fact]
        public void ToString_PathWithVariousObjects_CommonRepresentation()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(labels, objects);

            var pathStr = path.ToString();

            Assert.Equal("[1, v[1], hello]", pathStr);
        }

        [Fact]
        public void TryGetValue_KeyWithMultipleObjects_ReturnTrueAndObjects()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a", "b"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(labels, objects);

            object actualObj;
            var success = path.TryGetValue("b", out actualObj);

            Assert.True(success);
            Assert.Equal(new List<object> {1, new Vertex(1)}, actualObj);
        }

        [Fact]
        public void TryGetValue_OneObjectPerKey_ReturnTrueAndCorrectObject()
        {
            var labels = new List<List<string>>
            {
                new List<string> {"a"},
                new List<string> {"c", "b"},
                new List<string>()
            };
            var objects = new List<object> {1, new Vertex(1), "hello"};
            var path = new Path(labels, objects);

            object actualObj;
            var success = path.TryGetValue("b", out actualObj);

            Assert.True(success);
            Assert.Equal(new Vertex(1), actualObj);
        }

        [Fact]
        public void TryGetValue_UnknownKey_ReturnFalse()
        {
            var path = new Path(new List<List<string>>(), new List<object>());

            object targetObj;
            var success = path.TryGetValue("unknownKey", out targetObj);

            Assert.False(success);
        }
    }
}