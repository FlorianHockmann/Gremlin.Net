namespace Gremlin.Net.Process.Traversal
{
    public class TraversalPredicate
    {
        public TraversalPredicate(string operatorName, dynamic value, TraversalPredicate other = null)
        {
            OperatorName = operatorName;
            Value = value;
            Other = other;
        }

        public string OperatorName { get; set; }
        public dynamic Value { get; set; }
        public TraversalPredicate Other { get; set; }

        public TraversalPredicate And(TraversalPredicate otherPredicate)
        {
            return new TraversalPredicate("and", this, otherPredicate);
        }

        public TraversalPredicate Or(TraversalPredicate otherPredicate)
        {
            return new TraversalPredicate("or", this, otherPredicate);
        }

        public override string ToString()
        {
            return Other == null ? $"{OperatorName}({Value})" : $"{OperatorName}({Value},{Other})";
        }
    }
}