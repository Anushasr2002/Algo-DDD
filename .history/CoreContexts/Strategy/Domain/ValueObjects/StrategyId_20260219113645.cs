namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public readonly struct StrategyId
    {
        public Guid Value { get; }

        public StrategyId(Guid value) => Value = value;

        public static StrategyId NewId() => new StrategyId(Guid.NewGuid());

        public override string ToString() => Value.ToString();
    }
}
