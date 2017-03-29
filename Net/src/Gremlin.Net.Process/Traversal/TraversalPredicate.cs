namespace Gremlin.Net.Process.Traversal
{
    /// <summary>
    ///     Represents a predicate (boolean-valued function) used in a <see cref="Traversal" />.
    /// </summary>
    public class TraversalPredicate
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TraversalPredicate" /> class.
        /// </summary>
        /// <param name="operatorName">The name of the predicate.</param>
        /// <param name="value">The value of the predicate.</param>
        /// <param name="other">An optional other predicate that is used as an argument for this predicate.</param>
        public TraversalPredicate(string operatorName, dynamic value, TraversalPredicate other = null)
        {
            OperatorName = operatorName;
            Value = value;
            Other = other;
        }

        /// <summary>
        ///     Gets the name of the predicate.
        /// </summary>
        public string OperatorName { get; }

        /// <summary>
        ///     Gets the value of the predicate.
        /// </summary>
        public dynamic Value { get; }

        /// <summary>
        ///     Gets an optional other predicate that is used as an argument for this predicate.
        /// </summary>
        public TraversalPredicate Other { get; }

        /// <summary>
        ///     Returns a composed predicate that represents a logical AND of this predicate and another.
        /// </summary>
        /// <param name="otherPredicate">A predicate that will be logically-ANDed with this predicate.</param>
        /// <returns>The composed predicate.</returns>
        public TraversalPredicate And(TraversalPredicate otherPredicate)
        {
            return new TraversalPredicate("and", this, otherPredicate);
        }

        /// <summary>
        ///     Returns a composed predicate that represents a logical OR of this predicate and another.
        /// </summary>
        /// <param name="otherPredicate">A predicate that will be logically-ORed with this predicate.</param>
        /// <returns>The composed predicate.</returns>
        public TraversalPredicate Or(TraversalPredicate otherPredicate)
        {
            return new TraversalPredicate("or", this, otherPredicate);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Other == null ? $"{OperatorName}({Value})" : $"{OperatorName}({Value},{Other})";
        }
    }
}