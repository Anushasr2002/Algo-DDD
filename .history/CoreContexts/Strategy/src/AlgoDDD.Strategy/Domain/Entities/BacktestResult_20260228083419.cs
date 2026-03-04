namespace AlgoDDD.Strategy.Domain.Entities 
{
    public class BacktestResult
    {
        public Guid StrategyId { get; }
        public int Trades { get; }
        public decimal Profit { get; }

        public BacktestResult(Guid strategyId, int trades, decimal profit)
        {
            StrategyId = strategyId;
            Trades = trades;
            Profit = profit;
        }
    }
}

