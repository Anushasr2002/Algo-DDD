using System;

namespace AlgoDDD.Strategy.Domain.Entities
{
    public class Bar
    {
        public string Symbol { get; }
        public DateTime Timestamp { get; }
        public decimal Open { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public decimal Close { get; }
        public long Volume { get; }

        public Bar(string symbol, DateTime timestamp, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            Symbol = symbol;
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
