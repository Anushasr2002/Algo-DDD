using MediatR;
using AlgoDDD.MarketData.Domain.Entities;
using AlgoDDD.MarketData.Domain.ValueObjects;
using AlgoDDD.MarketData.Domain.Interfaces;

namespace AlgoDDD.MarketData.Application.Commands;

public class UpdateMarketDataCommand : IRequest<bool>
{
    public string Symbol { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public long Volume { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime Timestamp { get; set; }
}

public class UpdateMarketDataCommandHandler : IRequestHandler<UpdateMarketDataCommand, bool>
{
    private readonly IMarketDataRepository _repository;

    public UpdateMarketDataCommandHandler(IMarketDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateMarketDataCommand request, CancellationToken cancellationToken)
    {
        var symbol = new StockSymbol(request.Symbol, request.Exchange, request.Currency);
        
        var existingData = await _repository.GetLatestBySymbolAsync(symbol);
        
        if (existingData == null)
        {
            var newData = new StockData(
                symbol,
                new Price(request.CurrentPrice, request.Currency),
                new Price(request.OpenPrice, request.Currency),
                new Price(request.HighPrice, request.Currency),
                new Price(request.LowPrice, request.Currency),
                request.Volume,
                request.Timestamp
            );
            
            await _repository.AddAsync(newData);
        }
        else
        {
            existingData.UpdatePrice(
                new Price(request.CurrentPrice, request.Currency),
                request.Volume
            );
            
            await _repository.UpdateAsync(existingData);
        }
        
        return true;
    }
}
