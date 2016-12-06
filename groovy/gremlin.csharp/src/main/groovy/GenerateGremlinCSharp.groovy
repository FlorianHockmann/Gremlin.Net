
public class GenerateGremlinCSharp {

    public static void main(String[] args) {
        final String csharpDirectory = args[0]
        GraphTraversalSourceGenerator.create(csharpDirectory + "/Process/" + "GraphTraversalSource.cs")
        GraphTraversalGenerator.create(csharpDirectory + "/Process/" + "GraphTraversal.cs")
        AnonymousTraversalGenerator.create(csharpDirectory + "/Process/" + "__.cs")
        EnumGenerator.create(csharpDirectory + "/Process/")
    }
}