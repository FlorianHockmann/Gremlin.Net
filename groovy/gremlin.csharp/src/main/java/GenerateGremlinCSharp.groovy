
public class GenerateGremlinCSharp {

    public static void main(String[] args) {
        String csharpProcessDir = "../CSharp/Gremlin.CSharp/Process";
        GraphTraversalSourceGenerator.create("${csharpProcessDir}/GraphTraversalSource.cs");
        GraphTraversalGenerator.create("${csharpProcessDir}/GraphTraversal.cs");
        AnonymousTraversalGenerator.create("${csharpProcessDir}/__.cs");
    }
}