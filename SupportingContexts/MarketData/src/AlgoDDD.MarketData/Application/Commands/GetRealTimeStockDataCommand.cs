using MediatR;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.Interfaces;
using AlgoDDD.MarketData.Infrastructure.Services;

namespace AlgoDDD.MarketData.Application.Commands;

public class GetRealTimeStockDataCommand : IRequest<MarketData?>
{
    public string Symbol { get; set; } = string.Empty;
    public string Exchange { get; set; } = "NASDAQ";
    public bool ForceRefresh { get; set; }
}

public class GetRealTimeStockDataCommandHandler : IRequestHandler<GetRealTimeStockDataCommand, MarketData?>
{
    private readonly IMarketDataRepository _repository;
    private readonly StockDataService _stockDataService;

    public GetRealTimeStockDataCommandHandler(
        IMarketDataRepository repository,
        StockDataService stockDataService)
    {
        _repository = repository;
        _stockDataService = stockDataService;
    }

    public async Task<MarketData?> Handle(GetRealTimeStockDataCommand request, CancellationToken cancellationToken)
    {
        var symbol = new StockSymbol(request.Symbol, request.Exchange, "USD");
        
        // Check cache unless force refresh requested
        if (!request.ForceRefresh)
        {
            var cached = await _repository.GetLatestBySymbolAsync(symbol);
            if (cached != null && cached.LastUpdated > DateTime.UtcNow.AddMinutes(-1))
            {
                return cached; // Return cached data if less than 1 minute old
            }
        }

        // Fetch fresh data
        var freshData = await _stockDataService.GetStockQuoteAsync(request.Symbol, request.Exchange);
        
        if (freshData != null)
        {
            await _repository.AddAsync(freshData);
        }
        
        return freshData;
    }
}
