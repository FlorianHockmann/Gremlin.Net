using Gremlin.CSharp.Process;
using Xunit;

namespace Gremlin.CSharp.UnitTest
{
    public class PredicateTests
    {
        [Fact]
        public void ShouldKeepOrderForNestedPredicate()
        {
            Assert.Equal("and(eq(a),lt(b))", P.Eq("a").And(P.Lt("b")).ToString());
        }

        [Fact]
        public void ShouldKeepOrderForDoubleNestedPredicate()
        {
            Assert.Equal("and(or(lt(b),gt(c)),neq(d))", P.Lt("b").Or(P.Gt("c")).And(P.Neq("d")).ToString());
        }

        [Fact]
        public void ShouldKeepOrderForTripleNestedPredicate()
        {
            Assert.Equal("and(or(lt(b),gt(c)),or(neq(d),gte(e)))",
                P.Lt("b").Or(P.Gt("c")).And(P.Neq("d").Or(P.Gte("e"))).ToString());
        }
    }
}