using System;

namespace Gremlin.Net.Structure
{
    public class Property : IEquatable<Property>
    {
        public Property(string key, dynamic value, Element element)
        {
            Key = key;
            Value = value;
            Element = element;
        }

        public string Key { get; }
        public dynamic Value { get; }
        public Element Element { get; }

        public override string ToString()
        {
            return $"p[{Key}->{Value}]";
        }

        public bool Equals(Property other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Key, other.Key) && Equals(Value, other.Value) && Equals(Element, other.Element);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Key?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Element?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}