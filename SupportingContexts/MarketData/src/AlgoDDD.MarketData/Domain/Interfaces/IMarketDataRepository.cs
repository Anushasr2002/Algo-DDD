using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.MarketData.Domain.Interfaces;

public interface IMarketDataRepository
{
    Task<StockData?> GetLatestBySymbolAsync(StockSymbol symbol);
    Task<IEnumerable<StockData>> GetHistoryBySymbolAsync(StockSymbol symbol, DateTime from, DateTime to);
    Task AddAsync(StockData marketData);
    Task UpdateAsync(StockData marketData);
    Task DeleteAsync(string id);
}
