using Microsoft.AspNetCore.Mvc;              // For [ApiController], [Route], ControllerBase
using MediatR;                               // For IMediator
using Microsoft.AspNetCore.SignalR;          // For IHubContext<StrategyHub>
using AlgoDDD.Strategy.Application.Commands; // For CreateStrategyCommand
using AlgoDDD.Api.Hubs;                      // For StrategyHub

namespace AlgoDDD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StrategiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<StrategyHub> _hubContext;

        public StrategiesController(IMediator mediator, IHubContext<StrategyHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Creates a new trading strategy.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStrategyCommand command)
        {
            if (command == null)
                return BadRequest("Invalid strategy request.");

            var strategy = await _mediator.Send(command);

            // Broadcast to SignalR clients
            await _hubContext.Clients.All.SendAsync("StrategyCreated", strategy);

            return Ok(strategy);
        }
    }
}
