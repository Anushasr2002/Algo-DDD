using System;

namespace AlgoDDD.Strategy.Infrastructure.Entities
{
    public class EvaluationResultEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StrategyId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // store enum as string
        public decimal Price { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
