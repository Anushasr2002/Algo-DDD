using AlgoDDD.MarketData.Domain.ValueObjects;
using AlgoDDD.SharedKernel.Domain.BaseClasses;

namespace AlgoDDD.MarketData.Domain.Entities;

public class MarketData : Entity
{
    public StockSymbol Symbol { get; private set; }
    public Price CurrentPrice { get; private set; }
    public Price OpenPrice { get; private set; }
    public Price HighPrice { get; private set; }
    public Price LowPrice { get; private set; }
    public long Volume { get; private set; }
    public DateTime Timestamp { get; private set; }
    public DateTime LastUpdated { get; private set; }

    private MarketData() { } // For EF Core

    public MarketData(
        StockSymbol symbol, 
        Price currentPrice, 
        Price openPrice,
        Price highPrice,
        Price lowPrice,
        long volume,
        DateTime timestamp)
    {
        Id = Guid.NewGuid().ToString();
        Symbol = symbol;
        CurrentPrice = currentPrice;
        OpenPrice = openPrice;
        HighPrice = highPrice;
        LowPrice = lowPrice;
        Volume = volume;
        Timestamp = timestamp;
        LastUpdated = DateTime.UtcNow;
    }

    public void UpdatePrice(Price newPrice, long volume)
    {
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
