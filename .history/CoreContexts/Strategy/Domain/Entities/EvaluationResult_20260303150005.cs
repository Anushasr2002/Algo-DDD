using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class EvaluationResult
    {
        public Guid Id { get; private set; }
        public StrategyId StrategyId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Signal { get; private set; }
        public double Confidence { get; private set; }

        public EvaluationResult(StrategyId strategyId, string signal, double confidence)
        {
            Id = Guid.NewGuid();
            StrategyId = strategyId;
            Timestamp = DateTime.UtcNow;
            Signal = signal;
            Confidence = confidence;
        }
    }
}