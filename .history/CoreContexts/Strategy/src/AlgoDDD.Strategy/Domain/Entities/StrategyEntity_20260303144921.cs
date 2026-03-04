using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity
    {
        public StrategyId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public List<string> Parameters { get; private set; }

        // Constructor
        public StrategyEntity(StrategyId id, StrategyType type, List<string> parameters)
        {
            Id = id;
            Type = type;
            Parameters = parameters ?? new List<string>();
        }

        // Factory method
        public static StrategyEntity Create(string name, string description, StrategyType type, List<string> parameters)
        {
            var entity = new StrategyEntity(StrategyId.New(), type, parameters);
            entity.Name = name;
            entity.Description = description;
            return entity;
        }

        // Methods to update properties
        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}