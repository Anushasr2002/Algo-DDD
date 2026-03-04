namespace AlgoDDD.Strategy.Domain.ValueObjects {
{
    /// <summary>
    /// Represents a trading time frame (e.g., 1m, 5m, 1h, 1d).
    /// </summary>
    public class TimeFrame
    {
        public string Name { get; private set; }
        public TimeSpan Duration { get; private set; }

        public TimeFrame(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;
        }

        public override string ToString() => $"{Name} ({Duration})";
    }
}
}
