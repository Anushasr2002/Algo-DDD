using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Infrastructure.Configuration;

namespace AlgoDDD.MarketData.Infrastructure.Services;

public class StockDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<StockDataBackgroundService> _logger;
    private readonly MarketDataConfig _config;
    private readonly List<string> _trackedSymbols = new()
    {
        "AAPL", "MSFT", "GOOGL", "AMZN", "META",
        "JPM", "BAC", "WFC",
        "JNJ", "PFE", "MRK",
        "XOM", "CVX",
        "SPY", "QQQ"
    };

    public StockDataBackgroundService(
        IServiceProvider services,
        IOptions<MarketDataConfig> config,
        ILogger<StockDataBackgroundService> logger)
    {
        _services = services;
        _logger = logger;
        _config = config.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stock Data Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await UpdateStockData(stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(_config.CacheDurationMinutes), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Stock Data Background Service");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }

    private async Task UpdateStockData(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        var stockDataService = scope.ServiceProvider.GetRequiredService<StockDataService>();
        var repository = scope.ServiceProvider.GetRequiredService<IMarketDataRepository>();

        _logger.LogInformation("Updating stock data for {Count} symbols", _trackedSymbols.Count);

        var tasks = _trackedSymbols.Select(async symbol =>
        {
            try
            {
                var stockData = await stockDataService.GetStockQuoteAsync(symbol);
                if (stockData != null)
                {
                    await repository.AddAsync(stockData);
                    _logger.LogDebug("Updated data for {Symbol}: , Volume: {Volume:N0}", 
                        symbol, stockData.CurrentPrice.Value, stockData.Volume);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update {Symbol}", symbol);
            }
        });

        await Task.WhenAll(tasks);
    }
}

