namespace AlgoDDD.Strategy.Domain.Entities
{
    /// <summary>
    /// Represents the outcome of a backtest run for a strategy.
    /// </summary>
    public class BacktestResult
    {
        public Guid Id { get; private set; }
        public decimal ProfitLoss { get; private set; }
        public DateTime RunDate { get; private set; }

        public BacktestResult(Guid id, decimal profitLoss, DateTime runDate)
        {
            Id = id;
            ProfitLoss = profitLoss;
            RunDate = runDate;
        }
    }
}
