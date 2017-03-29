namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     Bindings are used to associate a variable with a value.
    /// </summary>
    public class Bindings
    {
        /// <summary>
        ///     Binds the variable to the specified value.
        /// </summary>
        /// <param name="variable">The variable to bind.</param>
        /// <param name="value">The value to which the variable should be bound.</param>
        /// <returns>The bound value.</returns>
        public Binding Of(string variable, object value)
        {
            return new Binding(variable, value);
        }
    }
}