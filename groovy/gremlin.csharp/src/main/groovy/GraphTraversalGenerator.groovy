import org.apache.tinkerpop.gremlin.process.traversal.dsl.graph.GraphTraversal

class GraphTraversalGenerator {

    public static void create(final String graphTraversalFile) {

        final StringBuilder csharpClass = new StringBuilder()

        csharpClass.append(CommonContentHelper.getLicense())

        csharpClass.append(
"""
using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.CSharp.Process
{
    public class GraphTraversal : Traversal
    {
        public GraphTraversal()
            : this(new List<ITraversalStrategy>(), new Bytecode())
        {
        }

        public GraphTraversal(IList<ITraversalStrategy> traversalStrategies, Bytecode bytecode)
        {
            TraversalStrategies = traversalStrategies;
            Bytecode = bytecode;
        }
""")
        GraphTraversal.getMethods().
                findAll { GraphTraversal.class.equals(it.returnType) }.
                findAll { !it.name.equals("clone") && !it.name.equals("iterate") }.
                collect { it.name }.
                unique().
                sort { a, b -> a <=> b }.
                forEach { javaMethodName ->
                    String sharpMethodName = SymbolHelper.toCSharp(javaMethodName)

                    csharpClass.append(
                            """
        public GraphTraversal ${sharpMethodName}(params object[] args)
        {
            Bytecode.AddStep("${javaMethodName}", args);
            return this;
        }
""")
                }
        csharpClass.append("\t}\n")
        csharpClass.append("}")

        final File file = new File(graphTraversalFile);
        file.delete()
        csharpClass.eachLine { file.append(it + "\n") }
    }
}
