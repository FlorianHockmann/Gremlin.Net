public final class SymbolHelper {

    public static String toCSharp(final String symbol) {
        return (String) Character.toUpperCase(symbol.charAt(0)) + symbol.substring(1)
    }

    public static String toJava(final String symbol) {
        return (String) Character.toLowerCase(symbol.charAt(0)) + symbol.substring(1)
    }
}
