using AlgoDDD.SharedKernel.Domain.BaseClasses;
using AlgoDDD.MarketData.Domain.ValueObjects;
using System;

namespace AlgoDDD.MarketData.Domain.Entities
{
    public class StockData : Entity<string>
    {
        public StockSymbol Symbol { get; private set; }
        public Price CurrentPrice { get; private set; }
        public Price OpenPrice { get; private set; }
        public Price HighPrice { get; private set; }
        public Price LowPrice { get; private set; }
        public long Volume { get; private set; }
        public DateTime Timestamp { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private StockData() : base(string.Empty)
        {
            Symbol = null!;
            CurrentPrice = null!;
            OpenPrice = null!;
            HighPrice = null!;
            LowPrice = null!;
        } // For EF Core

        public StockData(
            StockSymbol symbol,
            Price currentPrice,
            Price openPrice,
            Price highPrice,
            Price lowPrice,
            long volume,
            DateTime timestamp) : base(Guid.NewGuid().ToString())
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            CurrentPrice = currentPrice ?? throw new ArgumentNullException(nameof(currentPrice));
            OpenPrice = openPrice ?? throw new ArgumentNullException(nameof(openPrice));
            HighPrice = highPrice ?? throw new ArgumentNullException(nameof(highPrice));
            LowPrice = lowPrice ?? throw new ArgumentNullException(nameof(lowPrice));
            Volume = volume;
            Timestamp = timestamp;
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdatePrice(Price newPrice, long volume)
        {
            if (newPrice == null) throw new ArgumentNullException(nameof(newPrice));
            if (newPrice.Currency != CurrentPrice.Currency)
                throw new InvalidOperationException("Currency mismatch");

            CurrentPrice = newPrice;
            Volume = volume;

            if (newPrice.Value > HighPrice.Value)
                HighPrice = newPrice;

            if (newPrice.Value < LowPrice.Value)
                LowPrice = newPrice;

            LastUpdated = DateTime.UtcNow;
        }

        public decimal GetChangePercentage()
        {
            if (OpenPrice.Value == 0) return 0;
            return ((CurrentPrice.Value - OpenPrice.Value) / OpenPrice.Value) * 100;
        }
    }
}
