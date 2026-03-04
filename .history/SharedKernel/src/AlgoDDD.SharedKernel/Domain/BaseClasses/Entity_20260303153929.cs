using System;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity() { } // For EF Core

        public override bool Equals(object? obj)  // Add ? to fix nullability warning
        {
            if (obj is not Entity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id!.Equals(other.Id);  // Add ! if Id might be null
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;  // Handle null case
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)  // Add ? to fix nullability
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)  // Add ? to fix nullability
        {
            return !Equals(left, right);
        }
    }
}
