using MediatR;
using Microsoft.AspNetCore.Mvc;
using AlgoDDD.MarketData.Application.Queries;
using AlgoDDD.MarketData.Application.Commands;

namespace AlgoDDD.MarketData.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarketDataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{exchange}/{symbol}")]
    public async Task<IActionResult> GetStockPrice(string exchange, string symbol)
    {
        var query = new GetStockPriceQuery(symbol, exchange);
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();
            
        return Ok(new
        {
            symbol = result.Symbol.ToString(),
            price = result.CurrentPrice.Value,
            currency = result.CurrentPrice.Currency,
            open = result.OpenPrice.Value,
            high = result.HighPrice.Value,
            low = result.LowPrice.Value,
            volume = result.Volume,
            change = result.CurrentPrice.Value - result.OpenPrice.Value,
            changePercentage = result.GetChangePercentage(),
            timestamp = result.Timestamp,
            lastUpdated = result.LastUpdated
        });
    }

    [HttpGet("realtime/{symbol}")]
    public async Task<IActionResult> GetRealTimeData(string symbol, [FromQuery] string exchange = "NASDAQ", [FromQuery] bool forceRefresh = false)
    {
        var command = new GetRealTimeStockDataCommand
        {
            Symbol = symbol,
            Exchange = exchange,
            ForceRefresh = forceRefresh
        };
        
        var result = await _mediator.Send(command);
        
        if (result == null)
            return NotFound($"No data available for {symbol}");
            
        return Ok(new
        {
            symbol = result.Symbol.ToString(),
            price = result.CurrentPrice.Value,
            volume = result.Volume,
            high = result.HighPrice.Value,
            low = result.LowPrice.Value,
            timestamp = result.Timestamp,
            lastUpdated = result.LastUpdated
        });
    }

    [HttpGet("tracked-symbols")]
    public IActionResult GetTrackedSymbols()
    {
        var symbols = new[]
        {
            new { symbol = "AAPL", name = "Apple Inc.", exchange = "NASDAQ" },
            new { symbol = "MSFT", name = "Microsoft Corp.", exchange = "NASDAQ" },
            new { symbol = "GOOGL", name = "Alphabet Inc.", exchange = "NASDAQ" },
            new { symbol = "AMZN", name = "Amazon.com Inc.", exchange = "NASDAQ" },
            new { symbol = "META", name = "Meta Platforms Inc.", exchange = "NASDAQ" },
            new { symbol = "JPM", name = "JPMorgan Chase & Co.", exchange = "NYSE" },
            new { symbol = "BAC", name = "Bank of America Corp.", exchange = "NYSE" },
            new { symbol = "SPY", name = "SPDR S&P 500 ETF Trust", exchange = "NYSEARCA" },
            new { symbol = "QQQ", name = "Invesco QQQ Trust", exchange = "NASDAQ" }
        };
        
        return Ok(symbols);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateMarketData(UpdateMarketDataCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { success = result });
    }
}
