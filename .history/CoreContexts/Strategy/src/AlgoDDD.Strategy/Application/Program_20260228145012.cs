using Microsoft.Extensions.DependencyInjection;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Infrastructure.Services;

namespace AlgoDDD.Strategy.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Register MarketDataProvider as the implementation of IMarketDataProvider
            services.AddScoped<IMarketDataProvider, MarketDataProvider>();

            // Register StrategyEngine
            services.AddScoped<StrategyEngine>();

            var serviceProvider = services.BuildServiceProvider();

            // Example usage: resolve StrategyEngine
            var engine = serviceProvider.GetRequiredService<StrategyEngine>();

            // You can now call engine.ExecuteStrategyAsync(...) with a StrategyEntity
        }
    }
}
