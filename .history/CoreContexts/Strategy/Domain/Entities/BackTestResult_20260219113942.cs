namespace AlgoDDD.Strategy.Domain.Entities
{
    public class BacktestResult
    {
        public Guid Id { get; set; }
        public Guid StrategyId { get; set; }
        public DateTime RunDate { get; set; }
        public decimal ProfitLoss { get; set; }
        public int TradesExecuted { get; set; }
    }
}
