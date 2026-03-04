namespace AlgoDDD.Strategy.Domain.Entities
{
    public class SignalType
    {
        public StrategyId StrategyId { get; }
        public string Symbol { get; }
        public SignalType Type { get; }
        public decimal Price { get; }
        public string Description { get; }
        public DateTime Timestamp { get; }

        
    }

    public enum SignalType
    {
        Buy,
        Sell,
        Hold
    }
}
