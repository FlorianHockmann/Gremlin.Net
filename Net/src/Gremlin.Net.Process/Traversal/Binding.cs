using System;

namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     Associates a variable with a value.
    /// </summary>
    public class Binding : IEquatable<Binding>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Binding" /> class.
        /// </summary>
        /// <param name="key">The key that identifies the <see cref="Binding"/>.</param>
        /// <param name="value">The value of the <see cref="Binding"/>.</param>
        public Binding(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        ///     Gets the key that identifies the <see cref="Binding"/>.
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Gets the value of the <see cref="Binding"/>.
        /// </summary>
        public object Value { get; }

        /// <inheritdoc />
        public bool Equals(Binding other)
        {
            if (other == null)
                return false;
            return Key == other.Key && Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return Equals(other as Binding);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key?.GetHashCode() ?? 0) * 397) ^ (Value?.GetHashCode() ?? 0);
            }
        }
    }
}