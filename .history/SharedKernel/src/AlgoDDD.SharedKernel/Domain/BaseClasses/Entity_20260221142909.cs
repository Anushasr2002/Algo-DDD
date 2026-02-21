using System;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    // Generic entity base class
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() => Id?.GetHashCode() ?? 0;
    }

    // Non-generic convenience base class for Guid IDs
    public abstract class Entity : Entity<Guid>
    {
        protected Entity(Guid id) : base(id) { }
    }
}
