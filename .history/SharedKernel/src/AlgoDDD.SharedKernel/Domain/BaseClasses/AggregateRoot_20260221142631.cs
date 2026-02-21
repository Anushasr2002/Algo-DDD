using System;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    // AggregateRoot inherits from Entity<TId> and must call its constructor
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

        // Optional: domain events collection
        private readonly List<object> _domainEvents = new();

        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
