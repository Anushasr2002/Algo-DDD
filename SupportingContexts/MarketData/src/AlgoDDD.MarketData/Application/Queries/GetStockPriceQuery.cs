using MediatR;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.Interfaces;

namespace AlgoDDD.MarketData.Application.Queries;

public class GetStockPriceQuery : IRequest<StockData?>
{
    public string Symbol { get; }
    public string Exchange { get; }

    public GetStockPriceQuery(string symbol, string exchange)
    {
        Symbol = symbol.ToUpperInvariant();
        Exchange = exchange.ToUpperInvariant();
    }
}

public class GetStockPriceQueryHandler : IRequestHandler<GetStockPriceQuery, StockData?>
{
    private readonly IMarketDataRepository _repository;

    public GetStockPriceQueryHandler(IMarketDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<StockData?> Handle(GetStockPriceQuery request, CancellationToken cancellationToken)
    {
        var symbol = new StockSymbol(request.Symbol, request.Exchange, "USD");
        return await _repository.GetLatestBySymbolAsync(symbol);
    }
}
