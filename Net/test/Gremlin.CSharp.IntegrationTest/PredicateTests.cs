using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class PredicateTests
    {
        private readonly RemoteConnectionFactory _connectionFactory = new RemoteConnectionFactory();

        [Fact]
        public void PredicateAndTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var count = g.V().Has("age", P.Gt(30).And(P.Lt(35))).Count().Next();

            Assert.Equal((long) 1, count);
        }

        [Fact]
        public void SimplePWithinTest()
        {
            var graph = new Graph();
            var connection = _connectionFactory.CreateRemoteConnection();
            var g = graph.Traversal().WithRemote(connection);

            var count = g.V().Has("name", P.Within("josh", "vadas")).Count().Next();

            Assert.Equal((long) 2, count);
        }

        [Fact]
        public void NestedPredicateOrderTest()
        {
            Assert.Equal("and(eq(a),lt(b))", P.Eq("a").And(P.Lt("b")).ToString());
        }

        [Fact]
        public void DoubleNestedPredicateOrderTest()
        {
            Assert.Equal("and(or(lt(b),gt(c)),neq(d))", P.Lt("b").Or(P.Gt("c")).And(P.Neq("d")).ToString());
        }

        [Fact]
        public void TripleNestedPredicateOrderTest()
        {
            Assert.Equal("and(or(lt(b),gt(c)),or(neq(d),gte(e)))",
                P.Lt("b").Or(P.Gt("c")).And(P.Neq("d").Or(P.Gte("e"))).ToString());
        }
    }
}