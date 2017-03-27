using System;

namespace Gremlin.Net.Structure
{
    /// <summary>
    ///     A common base class for Graph elements.
    /// </summary>
    public abstract class Element : IEquatable<Element>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Element" /> class.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        /// <param name="label">The label of the element.</param>
        protected Element(object id, string label)
        {
            Id = id;
            Label = label;
        }

        /// <summary>
        ///     Gets the id of this <see cref="Element" />.
        /// </summary>
        public object Id { get; }

        /// <summary>
        ///     Gets the label of this <see cref="Element" />.
        /// </summary>
        public string Label { get; }

        /// <inheritdoc />
        public bool Equals(Element other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Element) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}