using System;

namespace Gremlin.Net.Process.Traversal
{
    public class Binding : IEquatable<Binding>
    {
        public Binding(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public object Value { get; }

        public bool Equals(Binding other)
        {
            if (other == null)
                return false;
            return Key == other.Key && Value.Equals(other.Value);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return Equals(other as Binding);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}