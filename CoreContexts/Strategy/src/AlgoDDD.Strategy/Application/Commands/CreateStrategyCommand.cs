using MediatR;
using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.ValueObjects;

namespace AlgoDDD.Strategy.Application.Commands;

public class CreateStrategyCommand : IRequest<Strategy>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public StrategyType Type { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public List<string> Symbols { get; set; } = new();
    public TimeFrame TimeFrame { get; set; }
    public decimal? MaxPositionSize { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
}

public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, Strategy>
{
    private readonly IStrategyRepository _repository;

    public CreateStrategyCommandHandler(IStrategyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Strategy> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
    {
        var strategy = new Strategy(
            request.Name,
            request.Description,
            request.Type,
            request.Parameters,
            request.Symbols,
            request.TimeFrame
        );

        return await _repository.AddAsync(strategy);
    }
}
