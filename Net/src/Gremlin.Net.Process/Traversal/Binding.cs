using System;
using System.Collections.Generic;

namespace Gremlin.Net.Process.Traversal
{
    public class Binding<TValue> : IEquatable<Binding<TValue>>
    {
        public string Key { get; private set; }
        public TValue Value { get; private set; }

        public Binding(string key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(Binding<TValue> other)
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
            return Equals(other as Binding<TValue>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key?.GetHashCode() ?? 0)*397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }
    }
}