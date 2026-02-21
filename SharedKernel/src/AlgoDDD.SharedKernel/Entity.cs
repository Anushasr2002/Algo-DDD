namespace AlgoDDD.SharedKernel
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        // Equality overrides for value-based comparison
        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() =>
            EqualityComparer<TId>.Default.GetHashCode(Id!);
    }
}
