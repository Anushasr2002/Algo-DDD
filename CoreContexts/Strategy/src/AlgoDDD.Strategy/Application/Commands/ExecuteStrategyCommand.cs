using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain;
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
        var StrategyEntity = await _repository.GetByIdAsync(request.StrategyId);
        if (StrategyEntity == null)
            throw new Exception($"StrategyEntity {request.StrategyId} not found");

        return await _strategyEngine.ExecuteStrategyAsync(StrategyEntity);
    }
}


