using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Domain.Aggregates
{
    /// <summary>
    /// Aggregate root for managing a trading strategy and its signals.
    /// </summary>
    public class StrategyAggregate
    {
        public StrategyEntity Strategy { get; private set; }
        private readonly List<Signal> _signals = new();

        public StrategyAggregate(StrategyEntity strategy)
        {
            Strategy = strategy;
        }

        public void AddSignal(Signal signal)
        {
            _signals.Add(signal);
        }

        public IReadOnlyCollection<Signal> Signals => _signals.AsReadOnly();
    }
}
