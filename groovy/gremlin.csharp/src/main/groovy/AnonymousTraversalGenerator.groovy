import org.apache.tinkerpop.gremlin.process.traversal.dsl.graph.GraphTraversal
import org.apache.tinkerpop.gremlin.process.traversal.dsl.graph.__

import java.lang.reflect.Modifier

class AnonymousTraversalGenerator {

    public static void create(final String anonymousTraversalFile) {

        final StringBuilder csharpClass = new StringBuilder()

        csharpClass.append(CommonContentHelper.getLicense())

        csharpClass.append(
"""
using Gremlin.Net.Process;

namespace Gremlin.CSharp.Process
{
    public static class __
    {
        public static GraphTraversal start()
        {
            return new GraphTraversal(new Bytecode());
        }

        public static GraphTraversal __(params object[] args)
        {
            return __.inject(args);
        }
""")
        __.getMethods().
                findAll { GraphTraversal.class.equals(it.returnType) }.
                findAll { Modifier.isStatic(it.getModifiers()) }.
                collect { it.name }.
                findAll { !it.equals("__") && !it.equals("start") }.
                unique().
                sort { a, b -> a <=> b }.
                forEach { javaMethodName ->
                    String sharpMethodName = SymbolHelper.toCSharp(javaMethodName)

                    csharpClass.append(
"""
        public static GraphTraversal ${sharpMethodName}(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).${sharpMethodName}(args);
        }
""")
                }
        csharpClass.append("\t}\n")
        csharpClass.append("}")

        final File file = new File(anonymousTraversalFile);
        file.delete()
        csharpClass.eachLine { file.append(it + "\n") }
    }
}
