using System;

namespace AlgoDDD.MarketData.Domain.Entities
{
    /// <summary>
    /// Represents a single OHLCV (Open, High, Low, Close, Volume) bar of market data.
    /// </summary>
    public class PriceBar
    {
        public DateTime Timestamp { get; private set; }
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public long Volume { get; private set; }

        public PriceBar(DateTime timestamp, decimal open, decimal high, decimal low, decimal close, long volume)
        {
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
