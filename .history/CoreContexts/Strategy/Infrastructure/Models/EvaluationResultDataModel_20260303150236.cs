using System;

namespace AlgoDDD.Strategy.Infrastructure.Models
{
    public class EvaluationResultDataModel
    {
        public Guid Id { get; set; }
        public Guid StrategyId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signal { get; set; }
        public double Confidence { get; set; }
    }
}