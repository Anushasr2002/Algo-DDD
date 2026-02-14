using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AlgoDDD.Strategy.Application.Commands;
using AlgoDDD.Strategy.API.Hubs;

namespace AlgoDDD.Strategy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StrategiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<StrategyHub> _hubContext;

    public StrategiesController(
        IMediator mediator,
        IHubContext<StrategyHub> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStrategy(CreateStrategyCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id}/execute")]
    public async Task<IActionResult> ExecuteStrategy(string id)
    {
        var command = new ExecuteStrategyCommand { StrategyId = id };
        var signals = await _mediator.Send(command);
        
        // Broadcast signals to connected clients
        foreach (var signal in signals)
        {
            await _hubContext.Clients.All.SendAsync("SignalGenerated", signal);
        }
        
        return Ok(signals);
    }

    [HttpPost("{id}/backtest")]
    public async Task<IActionResult> BacktestStrategy(string id, [FromBody] BacktestRequest request)
    {
        var command = new BacktestStrategyCommand
        {
            StrategyId = id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            InitialCapital = request.InitialCapital
        };
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}/signals")]
    public async Task<IActionResult> GetSignals(string id, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        // Implement get signals
        return Ok();
    }
}

public class BacktestRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal InitialCapital { get; set; }
}
