using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Infrastructure.Repositories;
using AlgoDDD.MarketData.Infrastructure.Services;
using AlgoDDD.MarketData.Infrastructure.Configuration;

namespace AlgoDDD.MarketData.API;

public static class DependencyInjection
{
    public static IServiceCollection AddMarketDataServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure settings
        services.Configure<MarketDataConfig>(
            configuration.GetSection("MarketData"));
        
        // Register repositories
        services.AddMemoryCache();
        services.AddScoped<IMarketDataRepository, InMemoryMarketDataRepository>();
        
        // Register HTTP client for stock data
        services.AddHttpClient<StockDataService>(client =>
        {
            var config = configuration.GetSection("MarketData").Get<MarketDataConfig>();
            client.BaseAddress = new Uri(config?.BaseUrl ?? "https://www.alphavantage.co/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        
        // Register background service for automatic updates
        services.AddHostedService<StockDataBackgroundService>();
        
        // Register MediatR handlers (if not already registered)
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        return services;
    }
}
