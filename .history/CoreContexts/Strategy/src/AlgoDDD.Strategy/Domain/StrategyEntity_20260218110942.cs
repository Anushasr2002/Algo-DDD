using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.SharedKernel.Domain.Events;
using AlgoDDD.Strategy.Domain.Events;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class StrategyEntity : Entity<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Parameters { get; private set; } = string.Empty;
        public List<string> Symbols { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }
        public string StrategyType { get; private set; } = "SMA"; // default

        public StrategyEntity(Guid id, string name, string description, string parameters, List<string> symbols, DateTime createdAt, string strategyType)
        {
            Id = id;
            Name = name;
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            CreatedAt = createdAt;
            StrategyType = strategyType;

            AddDomainEvent(new StrategyCreatedEvent(id, name, createdAt));
        }

        public void Update(string description, string parameters, List<string> symbols)
        {
            Description = description;
            Parameters = parameters;
            Symbols = symbols;
            AddDomainEvent(new StrategyUpdatedEvent(Id, DateTime.UtcNow));
        }

        public void Deactivate() => AddDomainEvent(new StrategyDeactivatedEvent(Id, DateTime.UtcNow));
        public void Execute() => AddDomainEvent(new StrategyExecutedEvent(Id, DateTime.UtcNow));

        private void AddDomainEvent(IDomainEvent domainEvent) => DomainEvents.Raise(domainEvent);
    }
}
