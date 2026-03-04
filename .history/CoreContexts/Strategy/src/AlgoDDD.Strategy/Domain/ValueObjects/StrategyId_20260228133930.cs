using AlgoDDD.SharedKernel;

public class StrategyId : ValueObject
{
    public Guid Value { get; }

    public StrategyId(Guid value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
