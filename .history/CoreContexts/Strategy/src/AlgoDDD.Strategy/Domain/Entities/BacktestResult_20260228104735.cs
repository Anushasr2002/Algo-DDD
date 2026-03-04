using System;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class BacktestResult
    {
        public StrategyId StrategyId { get; }
        public decimal ProfitLoss { get; }
        public int TradesExecuted { get; }
        public DateTime Timestamp { get; }
        public string Description { get; }

        public BacktestResult(
            StrategyId strategyId,
            decimal profitLoss,
            int tradesExecuted,
            DateTime timestamp,
            string description)
        {
            StrategyId = strategyId ?? throw new ArgumentNullException(nameof(strategyId));
            ProfitLoss = profitLoss;
            TradesExecuted = tradesExecuted;
            Timestamp = timestamp;
            Description = description ?? string.Empty;
        }

        // Convenience constructor for quick results
        public BacktestResult(StrategyId strategyId, decimal profitLoss, int tradesExecuted)
            : this(strategyId, profitLoss, tradesExecuted, DateTime.UtcNow, "Backtest completed")
        {
        }

        public override string ToString()
        {
            return $"Strategy {StrategyId}: Trades={TradesExecuted}, P/L={ProfitLoss}, Timestamp={Timestamp}";
        }
    }
}
