using AlgoDDD.SharedKernel.Domain.BaseClasses;  // Updated namespace
using AlgoDDD.Strategy.Domain.ValueObjects;
using System.Collections.Generic;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity<StrategyId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StrategyType Type { get; private set; }
        public List<string> Parameters { get; private set; }

        // Constructor
        public StrategyEntity(StrategyId id, StrategyType type, List<string> parameters) : base(id)
        {
            Type = type;
            Parameters = parameters ?? new List<string>();
        }

        // Private constructor for EF Core
        private StrategyEntity() { }

        // Factory method
        public static StrategyEntity Create(string name, string description, StrategyType type, List<string> parameters)
        {
            var entity = new StrategyEntity(StrategyId.New(), type, parameters);
            entity.Name = name;
            entity.Description = description;
            return entity;
        }

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}