using System;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class BacktestResult
    {
        // Example properties
        public int Id { get; set; }
        public DateTime RunDate { get; set; }
        public string StrategyName { get; set; }
        public decimal ProfitLoss { get; set; }

        // Example constructor
        public BacktestResult(int id, DateTime runDate, string strategyName, decimal profitLoss)
        {
            Id = id;
            RunDate = runDate;
            StrategyName = strategyName;
            ProfitLoss = profitLoss;
        }

        // Example method
        public override string ToString()
        {
            return $"{StrategyName} run on {RunDate}: P/L = {ProfitLoss}";
        }
    }
}
