using System;

namespace AlgoDDD.SharedKernel.Domain.BaseClasses
{
    public abstract class Entity
    {
        // Required ensures the property must be set during initialization
        public required Guid Id { get; set; }

        // Optional: track creation and modification timestamps
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        // Method to update timestamps
        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        // Equality based on Id
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
