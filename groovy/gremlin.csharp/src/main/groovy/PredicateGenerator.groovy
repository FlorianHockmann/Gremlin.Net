import org.apache.tinkerpop.gremlin.process.traversal.P

import java.lang.reflect.Modifier

class PredicateGenerator {

    public static void create(final String predicateFile) {

        final StringBuilder csharpClass = new StringBuilder()

        csharpClass.append(CommonContentHelper.getLicense())

        csharpClass.append(
"""
using Gremlin.Net.Process.Traversal;

namespace Gremlin.CSharp.Process
{
    public class P
    {""")
        P.class.getMethods().
                findAll { Modifier.isStatic(it.getModifiers()) }.
                findAll { P.class.isAssignableFrom(it.returnType) }.
                collect { it.name }.
                unique().
                sort { a, b -> a <=> b }.
                each { javaMethodName ->
                    String sharpMethodName = SymbolHelper.toCSharp(javaMethodName)
                    csharpClass.append(
"""
        public static TraversalPredicate ${sharpMethodName}(params object[] args)
        {
            var value = args.Length == 1 ? args[0] : args;
            return new TraversalPredicate("${javaMethodName}", value);
        }
""")
                }
        csharpClass.append("\t}\n")
        csharpClass.append("}")

        final File file = new File(predicateFile)
        file.delete()
        csharpClass.eachLine { file.append(it + "\n") }
    }
}
