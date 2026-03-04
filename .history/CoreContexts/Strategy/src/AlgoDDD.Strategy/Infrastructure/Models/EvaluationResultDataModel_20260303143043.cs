namespace AlgoDDD.Strategy.Infrastructure.Models
{
    public class EvaluationResultDataModel
    {
        public int Id { get; set; }
        public int StrategyId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signal { get; set; }
        public double Confidence { get; set; }
        // Add other properties
    }
}