using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gremlin.Net.Process.Traversal.Strategy
{
    /// <summary>
    ///     Provides a common base class for strategies that are only included in <see cref="Bytecode" />
    ///     to be applied remotely.
    /// </summary>
    public abstract class AbstractTraversalStrategy : ITraversalStrategy, IEquatable<AbstractTraversalStrategy>
    {
        /// <summary>
        ///     Gets the name of the strategy.
        /// </summary>
        public string StrategyName => GetType().Name;

        /// <summary>
        ///     Gets the configuration of the strategy.
        /// </summary>
        public Dictionary<string, dynamic> Configuration { get; } = new Dictionary<string, dynamic>();

        /// <inheritdoc />
        public bool Equals(AbstractTraversalStrategy other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StrategyName == other.StrategyName;
        }

        /// <inheritdoc />
        public virtual void Apply(Traversal traversal)
        {
        }

        /// <inheritdoc />
        public virtual Task ApplyAsync(Traversal traversal)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AbstractTraversalStrategy) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return StrategyName.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return StrategyName;
        }
    }
}