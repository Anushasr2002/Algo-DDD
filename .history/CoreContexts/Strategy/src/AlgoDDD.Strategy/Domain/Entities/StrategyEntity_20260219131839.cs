using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity<StrategyId>
    {
        public string Name { get; private set; }

        public StrategyEntity(StrategyId id, string name) : base(id)
        {
            Name = name;
        }
    }
}
