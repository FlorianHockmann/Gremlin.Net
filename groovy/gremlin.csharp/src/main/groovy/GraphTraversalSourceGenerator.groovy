import org.apache.tinkerpop.gremlin.process.traversal.dsl.graph.GraphTraversal
import org.apache.tinkerpop.gremlin.process.traversal.dsl.graph.GraphTraversalSource


class GraphTraversalSourceGenerator {

    public static void create(final String graphTraversalSourceFile) {

        final StringBuilder csharpClass = new StringBuilder()

        csharpClass.append(CommonContentHelper.getLicense())

        csharpClass.append(
"""
using System.Collections.Generic;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.CSharp.Process
{
    public class GraphTraversalSource
    {
        public IList<ITraversalStrategy> TraversalStrategies { get; set; }
        public Bytecode Bytecode { get; set; }

         public GraphTraversalSource()
            : this(new List<ITraversalStrategy>(), new Bytecode())
        {
        }

        public GraphTraversalSource(IList<ITraversalStrategy> traversalStrategies, Bytecode bytecode)
        {
            TraversalStrategies = traversalStrategies;
            Bytecode = bytecode;
        }
"""
        )

        // Hold the list of methods with their overloads, so we do not create duplicates
        HashMap<String, ArrayList<String>> sharpMethods = new HashMap<String, ArrayList<String>>()

        GraphTraversalSource.getMethods(). // SOURCE STEPS
                findAll { GraphTraversalSource.class.equals(it.returnType) }.
                findAll {
                    !it.name.equals("clone") &&
                            // replace by TraversalSource.Symbols.XXX
                            !it.name.equals("withBindings") &&
                            !it.name.equals("withRemote") &&
                            !it.name.equals("withComputer")
                }.
                collect { it.name }.
                unique().
                sort { a, b -> a <=> b }.
                forEach { javaMethodName ->
                    String sharpMethodName = SymbolHelper.toCSharp(javaMethodName)

                    csharpClass.append(
"""
        public GraphTraversalSource ${sharpMethodName}(params object[] args)
        {
            var source = new GraphTraversalSource(TraversalStrategies, Bytecode);
            source.Bytecode.AddSource("${javaMethodName}\", args);
            return source;
        }
""")
                }

        csharpClass.append(
                """
        public GraphTraversalSource WithBindings(object bindings)
        {
            return this;
        }
""")    // ToDo: Add methods for withRemote() and withComputer()

        GraphTraversalSource.getMethods(). // SPAWN STEPS
                findAll { GraphTraversal.class.equals(it.returnType) }.
                collect { it.name }.
                unique().
                sort { a, b -> a <=> b }.
                forEach { javaMethodName ->
                    String sharpMethodName = SymbolHelper.toCSharp(javaMethodName)

                    csharpClass.append(
                            """
        public GraphTraversal ${sharpMethodName}(params object[] args)
        {
            var traversal = new GraphTraversal(TraversalStrategies, Bytecode);
            traversal.Bytecode.AddStep("${javaMethodName}\", args);
            return traversal;
        }
""")
                }

        csharpClass.append("\t}\n")
        csharpClass.append("}")

        final File file = new File(graphTraversalSourceFile);
        file.delete()
        csharpClass.eachLine { file.append(it + "\n") }
    }
}