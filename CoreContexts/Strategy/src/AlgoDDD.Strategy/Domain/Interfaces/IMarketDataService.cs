using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Domain.Interfaces;

public interface IMarketDataService
{
    Task<StockData?> GetLatestMarketDataAsync(StockSymbol symbol);
    Task<IEnumerable<StockData>> GetHistoricalDataAsync(StockSymbol symbol, int days);
}
