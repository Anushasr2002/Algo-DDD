namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public readonly struct StrategyId : IEquatable<StrategyId>
    {
        public Guid Value { get; }

        public StrategyId(Guid value) => Value = value;

        public static StrategyId NewId() => new StrategyId(Guid.NewGuid());

        public override string ToString() => Value.ToString();

        public bool Equals(StrategyId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is StrategyId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(StrategyId left, StrategyId right) => left.Equals(right);
        public static bool operator !=(StrategyId left, StrategyId right) => !(left == right);
    }
}
