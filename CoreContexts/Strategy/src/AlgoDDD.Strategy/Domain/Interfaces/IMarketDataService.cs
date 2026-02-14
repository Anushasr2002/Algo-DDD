using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces;

public interface IMarketDataService
{
    Task<MarketData?> GetLatestMarketDataAsync(StockSymbol symbol);
    Task<IEnumerable<MarketData>> GetHistoricalDataAsync(StockSymbol symbol, int days);
}
