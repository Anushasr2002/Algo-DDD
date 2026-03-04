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

        public Signal(StrategyId strategyId, string symbol, SignalType type, decimal price, string description, DateTime timestamp)
        {
            StrategyId = strategyId ?? throw new ArgumentNullException(nameof(strategyId));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Type = type;
            Price = price;
            Description = description ?? string.Empty;
            Timestamp = timestamp;
        }
    }

    public enum SignalType
    {
        Buy,
        Sell,
        Hold
    }
}
