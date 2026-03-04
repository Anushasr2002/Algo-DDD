using System;
using AlgoDDD.SharedKernel;


namespace AlgoDDD.Strategy.Domain.Entities
{
    public class SignalType
    {
        public StrategyId StrategyId { get; }
        public string Symbol { get; }
        public SignalAction Action { get; }
        public decimal Price { get; }
        public string Description { get; }
        public DateTime Timestamp { get; }

        public SignalType(
            StrategyId strategyId,
            string symbol,
            SignalAction action,
            decimal price,
            string description,
            DateTime timestamp)
        {
            StrategyId = strategyId ?? throw new ArgumentNullException(nameof(strategyId));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Action = action;
            Price = price;
            Description = description ?? string.Empty;
            Timestamp = timestamp;
        }
    }

    public enum SignalAction
    {
        Buy,
        Sell,
        Hold
    }
}
