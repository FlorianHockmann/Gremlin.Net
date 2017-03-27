using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Structure
{
    /// <summary>
    ///     A Path denotes a particular walk through a graph as defined by a <see cref="Traversal" />.
    /// </summary>
    /// <remarks>In abstraction, any Path implementation maintains two lists: a list of sets of labels and a list of objects.
    /// The list of labels are the labels of the steps traversed. The list of objects are the objects traversed.</remarks>
    public class Path : IReadOnlyList<object>, IEquatable<Path>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Path" /> class.
        /// </summary>
        /// <param name="labels">The labels associated with the path</param>
        /// <param name="objects">The objects in the <see cref="Path" />.</param>
        public Path(List<List<string>> labels, List<object> objects)
        {
            Labels = labels;
            Objects = objects;
        }

        /// <summary>
        ///     Gets an ordered list of the labels associated with the <see cref="Path" />.
        /// </summary>
        public List<List<string>> Labels { get; }

        /// <summary>
        ///     Gets an ordered list of the objects in the <see cref="Path" />.
        /// </summary>
        public List<object> Objects { get; }

        public object this[string label]
        {
            get
            {
                object obj;
                var objFound = TryGetValue(label, out obj);
                if (!objFound)
                    throw new KeyNotFoundException($"The step with label {label} does not exist");
                return obj;
            }
        }

        /// <inheritdoc />
        public bool Equals(Path other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ObjectsEqual(other.Objects) && LabelsEqual(other.Labels);
        }

        public dynamic this[int index] => Objects[index];

        public int Count => Objects.Count;

        /// <inheritdoc />
        public IEnumerator<object> GetEnumerator()
        {
            return ((IReadOnlyList<object>) Objects).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IReadOnlyList<object>) Objects).GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{string.Join(", ", Objects)}]";
        }

        public bool ContainsKey(string key)
        {
            return Labels.Any(objLabels => objLabels.Contains(key));
        }

        public bool TryGetValue(string label, out object value)
        {
            value = null;
            for (var i = 0; i < Labels.Count; i++)
            {
                if (!Labels[i].Contains(label)) continue;
                if (value == null)
                    value = Objects[i];
                else if (value.GetType() == typeof(List<object>))
                    ((List<object>) value).Add(Objects[i]);
                else
                    value = new List<object> {value, Objects[i]};
            }
            return value != null;
        }

        private bool ObjectsEqual(IReadOnlyCollection<object> otherObjects)
        {
            if (Objects == null)
                return otherObjects == null;
            return Objects.SequenceEqual(otherObjects);
        }

        private bool LabelsEqual(IReadOnlyList<List<string>> otherLabels)
        {
            if (Labels == null)
                return otherLabels == null;
            if (Labels.Count != otherLabels.Count)
                return false;
            var foundUnequalObjLabels = Labels.Where((objLabels, i) => !objLabels.SequenceEqual(otherLabels[i])).Any();
            return !foundUnequalObjLabels;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Path) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 19;
                if (Labels != null)
                    hashCode = Labels.Where(objLabels => objLabels != null)
                        .Aggregate(hashCode,
                            (current1, objLabels) => objLabels.Aggregate(current1,
                                (current, label) => current * 31 + label.GetHashCode()));
                if (Objects != null)
                    hashCode = Objects.Aggregate(hashCode, (current, obj) => current * 31 + obj.GetHashCode());
                return hashCode;
            }
        }
    }
}