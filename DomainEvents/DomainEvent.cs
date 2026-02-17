namespace AlgoDDD.SharedKernel
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
        public string EventType => GetType().Name;
    }
}
