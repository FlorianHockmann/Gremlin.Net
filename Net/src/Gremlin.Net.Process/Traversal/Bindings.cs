namespace Gremlin.Net.Process.Traversal
{
    public class Bindings
    {
        /// <summary>
        ///     Binds the variable to the specified value.
        /// </summary>
        /// <param name="variable">The variable to bind.</param>
        /// <param name="value">The value to which the variable should be bound.</param>
        /// <returns>The original value.</returns>
        public Binding Of(string variable, object value)
        {
            return new Binding(variable, value);
        }
    }
}