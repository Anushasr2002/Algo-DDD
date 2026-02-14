using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;

namespace AlgoDDD.Strategy.Application.Commands;

public class ExecuteStrategyCommand : IRequest<List<Signal>>
{
    public string StrategyId { get; set; } = string.Empty;
}

public class ExecuteStrategyCommandHandler : IRequestHandler<ExecuteStrategyCommand, List<Signal>>
{
    private readonly IStrategyRepository _repository;
    private readonly StrategyEngine _strategyEngine;

    public ExecuteStrategyCommandHandler(
        IStrategyRepository repository,
        StrategyEngine strategyEngine)
    {
        _repository = repository;
        _strategyEngine = strategyEngine;
    }

    public async Task<List<Signal>> Handle(ExecuteStrategyCommand request, CancellationToken cancellationToken)
    {
        var strategy = await _repository.GetByIdAsync(request.StrategyId);
        if (strategy == null)
            throw new Exception($"Strategy {request.StrategyId} not found");

        return await _strategyEngine.ExecuteStrategyAsync(strategy);
    }
}
