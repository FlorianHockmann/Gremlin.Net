import org.apache.tinkerpop.gremlin.util.CoreImports

class EnumGenerator {

    public static void create(final String enumDirectory) {

        for (final Class<? extends Enum> enumClass : CoreImports.getClassImports()
                .findAll { Enum.class.isAssignableFrom(it) }
                .sort { a, b -> a.getSimpleName() <=> b.getSimpleName() }
                .collect()) {
            createEnum(enumDirectory, enumClass)
        }
    }

    private static void createEnum(final String enumDirectory, final Class<? extends Enum> enumClass){
        final StringBuilder csharpEnum = new StringBuilder()

        csharpEnum.append(CommonContentHelper.getLicense())

        csharpEnum.append(
"""
namespace Gremlin.CSharp.Process
{
    public enum ${enumClass.getSimpleName()}
    {
""")
        enumClass.getEnumConstants()
                .sort { a, b -> a.name() <=> b.name() }
                .each { value -> csharpEnum.append("        ${SymbolHelper.toCSharp(value.name())},\n"); }
        csharpEnum.deleteCharAt(csharpEnum.length() - 2)

        csharpEnum.append("    }\n")
        csharpEnum.append("}")

        final String enumFileName = "${enumDirectory}/${enumClass.getSimpleName()}.cs"
        final File file = new File(enumFileName);
        file.delete()
        csharpEnum.eachLine { file.append(it + "\n") }
    }
}
