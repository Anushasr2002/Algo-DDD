using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Infrastructure.Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Register the in‑memory provider
        services.AddSingleton<IMarketDataProvider, InMemoryMarketDataProvider>();
        services.AddSingleton<IMarketDataProvider, InMemoryMarketDataProvider>();


        // Register StrategyEngine
        services.AddScoped<StrategyEngine>();

        // Add controllers, etc.
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Middleware pipeline
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
