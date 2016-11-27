using System.Collections.Generic;

namespace Gremlin.Net.Process
{
    public class Bindings
    {
        /// <summary>
        /// Binds the variable to the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="variable">The variable to bind.</param>
        /// <param name="value">The value to which the variable should be bound.</param>
        /// <returns>The original value.</returns>
        public Binding<TValue> Of<TValue>(string variable, TValue value)
        {
            return new Binding<TValue>(variable, value);
        }


    }
}