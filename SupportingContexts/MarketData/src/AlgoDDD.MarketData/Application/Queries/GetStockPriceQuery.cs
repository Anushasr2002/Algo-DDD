using MediatR;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;

namespace AlgoDDD.MarketData.Application.Queries;

public class GetStockPriceQuery : IRequest<MarketData?>
{
    public string Symbol { get; }
    public string Exchange { get; }

    public GetStockPriceQuery(string symbol, string exchange)
    {
        Symbol = symbol.ToUpperInvariant();
        Exchange = exchange.ToUpperInvariant();
    }
}

public class GetStockPriceQueryHandler : IRequestHandler<GetStockPriceQuery, MarketData?>
{
    private readonly IMarketDataRepository _repository;

    public GetStockPriceQueryHandler(IMarketDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<MarketData?> Handle(GetStockPriceQuery request, CancellationToken cancellationToken)
    {
        var symbol = new StockSymbol(request.Symbol, request.Exchange, "USD"); // Default currency
        return await _repository.GetLatestBySymbolAsync(symbol);
    }
}
