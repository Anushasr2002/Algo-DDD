using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class BacktestResult
    {
        public Guid Id { get; private set; }
        public StrategyId StrategyId { get; private set; }
        public DateTime RunDate { get; private set; }
        public decimal ProfitLoss { get; private set; }
        public int TradesExecuted { get; private set; }

        // EF Core requires a private parameterless constructor
        private BacktestResult()
        {
            Id = Guid.NewGuid();
            StrategyId = StrategyId.NewId();
            RunDate = DateTime.UtcNow;
        }

        // Domain constructor for creating a backtest result
        public BacktestResult(StrategyId strategyId, decimal profitLoss, int tradesExecuted)
        {
            Id = Guid.NewGuid();
            StrategyId = strategyId ?? throw new ArgumentNullException(nameof(strategyId));
            RunDate = DateTime.UtcNow;
            ProfitLoss = profitLoss;
            TradesExecuted = tradesExecuted;
        }
    }
}
