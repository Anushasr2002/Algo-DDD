using System;

namespace AlgoDDD.Strategy.Domain.ValueObjects
{
    public class TimeFrame
    {
        // Example properties
        public string Name { get; private set; }
        public TimeSpan Duration { get; private set; }

        // Example constructor
        public TimeFrame(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;
        }

        // Example method
        public override string ToString()
        {
            return $"{Name} ({Duration})";
        }
    }
}
