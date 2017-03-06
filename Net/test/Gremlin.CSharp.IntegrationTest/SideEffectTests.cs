using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Driver.Remote;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class SideEffectTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void Get_NonExistingKey_Throw()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Iterate();

            Assert.Throws<KeyNotFoundException>(() => t.SideEffects.Get("m"));
        }

        [Fact]
        public void Keys_NamedGroupCount_SideEffectKey()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Out("created").GroupCount("m").By("name").Iterate();

            var keys = t.SideEffects.Keys();

            var keysList = keys.ToList();
            Assert.Equal(1, keysList.Count);
            Assert.Contains("m", keysList);
        }

        [Fact]
        public void Get_NamedGroupCount_SideEffectValue()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Out("created").GroupCount("m").By("name").Iterate();
            t.SideEffects.Keys();

            var m = t.SideEffects.Get("m") as Dictionary<string, dynamic>;

            Assert.Equal(2, m.Count);
            Assert.Equal((long) 3, m["lop"]);
            Assert.Equal((long) 1, m["ripple"]);
        }

        [Fact]
        public void Get_NamedList_SideEffectValue()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.WithSideEffect("a", new List<string> {"first", "second"}).V().Iterate();
            t.SideEffects.Keys();

            var a = t.SideEffects.Get("a") as List<object>;

            Assert.Equal(2, a.Count);
            Assert.Equal("first", a[0]);
            Assert.Equal("second", a[1]);
        }

        [Fact]
        public void Get_TraversalWithTwoSideEffects_GetBothSideEffects()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var t = g.V().Out("created").GroupCount("m").By("name").Values("name").Aggregate("n").Iterate();

            var keys = t.SideEffects.Keys().ToList();
            Assert.Equal(2, keys.Count);
            Assert.Contains("m", keys);
            Assert.Contains("n", keys);
            var n = (Dictionary<object, long>)t.SideEffects.Get("n");
            Assert.Equal(2, n.Count);
            Assert.Equal(3, n["lop"]);
            Assert.Equal(1, n["ripple"]);
        }

        [Fact]
        public void Get_ClosedSideEffects_Throw()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Aggregate("a").Iterate();

            t.SideEffects.Close();
            Assert.Throws<InvalidOperationException>(() => t.SideEffects.Get("a"));
        }

        [Fact]
        public void Get_CloseAfterSameGet_GetCachedSideEffect()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Aggregate("a").Iterate();

            t.SideEffects.Get("a");
            t.SideEffects.Close();
            var results = t.SideEffects.Get("a");

            Assert.NotNull(results);
        }

        [Fact]
        public void Keys_CloseAfterGet_CachedKeys()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);
            var t = g.V().Aggregate("a").Aggregate("b").Iterate();

            t.SideEffects.Get("a");
            t.SideEffects.Close();
            var keys = t.SideEffects.Keys();

            Assert.Equal(2, keys.Count);
            Assert.Contains("a", keys);
            Assert.Contains("b", keys);
        }
    }
}