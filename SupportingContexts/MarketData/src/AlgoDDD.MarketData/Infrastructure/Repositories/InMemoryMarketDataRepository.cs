using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.MarketData.Infrastructure.Repositories;

public class InMemoryMarketDataRepository : IMarketDataRepository
{
    private readonly ConcurrentDictionary<string, StockData> _latestData = new();
    private readonly ConcurrentDictionary<string, List<StockData>> _history = new();
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public InMemoryMarketDataRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<StockData?> GetLatestBySymbolAsync(StockSymbol symbol)
    {
        var key = symbol.ToString();
        
        if (_cache.TryGetValue<StockData>(key, out var cachedData))
        {
            return Task.FromResult(cachedData);
        }
        
        _latestData.TryGetValue(key, out var data);
        
        if (data != null)
        {
            _cache.Set(key, data, _cacheDuration);
        }
        
        return Task.FromResult(data);
    }

    public Task<IEnumerable<StockData>> GetHistoryBySymbolAsync(StockSymbol symbol, DateTime from, DateTime to)
    {
        var key = symbol.ToString();
        
        if (_history.TryGetValue(key, out var history))
        {
            var filtered = history.Where(x => x.Timestamp >= from && x.Timestamp <= to)
                                  .OrderByDescending(x => x.Timestamp)
                                  .AsEnumerable();
            return Task.FromResult(filtered);
        }
        
        return Task.FromResult(Enumerable.Empty<StockData>());
    }

    public Task AddAsync(StockData stockData)
    {
        var key = stockData.Symbol.ToString();
        
        _latestData.AddOrUpdate(key, stockData, (_, _) => stockData);
        _cache.Set(key, stockData, _cacheDuration);
        
        var history = _history.GetOrAdd(key, _ => new List<StockData>());
        history.Add(stockData);
        
        if (history.Count > 100)
        {
            history.RemoveRange(0, history.Count - 100);
        }
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(StockData stockData)
    {
        return AddAsync(stockData);
    }

    public Task DeleteAsync(string id)
    {
        var item = _latestData.FirstOrDefault(x => x.Value.Id == id);
        if (!string.IsNullOrEmpty(item.Key))
        {
            _latestData.TryRemove(item.Key, out _);
            _cache.Remove(item.Key);
        }
        
        return Task.CompletedTask;
    }
}
