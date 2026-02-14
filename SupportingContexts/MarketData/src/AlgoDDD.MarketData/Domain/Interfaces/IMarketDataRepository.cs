using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.MarketData.Domain.Interfaces;

public interface IMarketDataRepository
{
    Task<MarketData?> GetLatestBySymbolAsync(StockSymbol symbol);
    Task<IEnumerable<MarketData>> GetHistoryBySymbolAsync(StockSymbol symbol, DateTime from, DateTime to);
    Task AddAsync(MarketData marketData);
    Task UpdateAsync(MarketData marketData);
    Task DeleteAsync(string id);
}
