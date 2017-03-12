using System.Collections.Generic;
using Gremlin.CSharp.Process;
using Gremlin.CSharp.Structure;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
using Gremlin.Net.Process.Traversal.Strategy.Finalization;
using Gremlin.Net.Process.Traversal.Strategy.Optimization;
using Gremlin.Net.Process.Traversal.Strategy.Verification;
using Xunit;

namespace Gremlin.CSharp.IntegrationTest
{
    public class StrategiesTest
    {
        [Fact]
        public void TraversalWithoutStrategies_AfterWithStrategiesWasCalled_WithStrategiesNotAffected()
        {
            var graph = new Graph();
            var g = graph.Traversal();
            g.WithStrategies(new ReadOnlyStrategy(), new IncidentToAdjacentStrategy());

            var bytecode = g.WithoutStrategies(new ReadOnlyStrategy()).Bytecode;

            Assert.Equal(2, bytecode.SourceInstructions.Count);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(2, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal(new IncidentToAdjacentStrategy(), bytecode.SourceInstructions[0].Arguments[1]);

            Assert.Equal("withoutStrategies", bytecode.SourceInstructions[1].OperatorName);
            Assert.Equal(1, bytecode.SourceInstructions[1].Arguments.Length);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[1].Arguments[0]);
        }

        [Fact]
        public void TraversalWithoutStrategies_MultipleStrategies_IncludeInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithoutStrategies(new ReadOnlyStrategy(), new LazyBarrierStrategy()).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(2, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withoutStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal(new LazyBarrierStrategy(), bytecode.SourceInstructions[0].Arguments[1]);
        }

        [Fact]
        public void TraversalWithoutStrategies_OneStrategy_IncludeInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithoutStrategies(new ReadOnlyStrategy()).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(1, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withoutStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
        }

        [Fact]
        public void TraversalWithStrategies_ConfigurableMatchAlgorithmStrategy_IncludeConfigInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithStrategies(new MatchAlgorithmStrategy("greedy")).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(1, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new MatchAlgorithmStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
        }

        [Fact]
        public void TraversalWithStrategies_MultipleStrategies_IncludeInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithStrategies(new ReadOnlyStrategy(), new IncidentToAdjacentStrategy()).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(2, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal(new IncidentToAdjacentStrategy(), bytecode.SourceInstructions[0].Arguments[1]);
        }

        [Fact]
        public void TraversalWithStrategies_OneStrategy_IncludeInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithStrategies(new ReadOnlyStrategy()).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(1, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal("ReadOnlyStrategy", bytecode.SourceInstructions[0].Arguments[0].ToString());
            Assert.Equal(new ReadOnlyStrategy().GetHashCode(), bytecode.SourceInstructions[0].Arguments[0].GetHashCode());
            Assert.Equal(0, g.TraversalStrategies.Count);
        }

        [Fact]
        public void TraversalWithStrategies_Strategies_ApplyToReusedGraphTraversalSource()
        {
            var graph = new Graph();
            var g = graph.Traversal();
            g.WithStrategies(new ReadOnlyStrategy(), new IncidentToAdjacentStrategy());

            var bytecode = g.V().Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(2, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new ReadOnlyStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            Assert.Equal(new IncidentToAdjacentStrategy(), bytecode.SourceInstructions[0].Arguments[1]);
            Assert.Equal(1, bytecode.StepInstructions.Count);
            Assert.Equal("V", bytecode.StepInstructions[0].OperatorName);
        }

        [Fact]
        public void TraversalWithStrategies_StrategyWithTraversalInConfig_IncludeTraversalInInConfigInBytecode()
        {
            var graph = new Graph();
            var g = graph.Traversal();

            var bytecode = g.WithStrategies(new SubgraphStrategy(__.Has("name", "marko"))).Bytecode;

            Assert.Equal(1, bytecode.SourceInstructions.Count);
            Assert.Equal(1, bytecode.SourceInstructions[0].Arguments.Length);
            Assert.Equal("withStrategies", bytecode.SourceInstructions[0].OperatorName);
            Assert.Equal(new SubgraphStrategy(), bytecode.SourceInstructions[0].Arguments[0]);
            SubgraphStrategy strategy = bytecode.SourceInstructions[0].Arguments[0];
            Assert.Equal(1, strategy.Configuration.Count);
            Assert.Equal(typeof(GraphTraversal), strategy.Configuration["vertices"].GetType());
            GraphTraversal traversal = strategy.Configuration["vertices"];
            Assert.Equal("has", traversal.Bytecode.StepInstructions[0].OperatorName);
            Assert.Equal(new List<string> {"name", "marko"}, traversal.Bytecode.StepInstructions[0].Arguments);
        }
    }
}