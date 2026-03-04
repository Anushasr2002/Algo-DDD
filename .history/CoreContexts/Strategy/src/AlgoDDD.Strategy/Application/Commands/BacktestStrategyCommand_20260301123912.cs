using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain;
using MediatR;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Application.Commands {

public class BacktestStrategyCommand : IRequest<BacktestResult>
{
    public string StrategyId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal InitialCapital { get; set; }
}

public class BacktestStrategyCommandHandler : IRequestHandler<BacktestStrategyCommand, BacktestResult>
{
    private readonly IStrategyRepository _repository;
    private readonly StrategyEngine _strategyEngine;

    public BacktestStrategyCommandHandler(
        IStrategyRepository repository,
        StrategyEngine strategyEngine)
    {
       
        _repository = repository;
        _strategyEngine = strategyEngine;
    }

    public async Task<BacktestResult> Handle(BacktestStrategyCommand request, CancellationToken cancellationToken)
    {
        var StrategyEntity = await _repository.GetByIdAsync(request.StrategyId);
        if (StrategyEntity == null)
            throw new Exception($"StrategyEntity {request.StrategyId} not found");

        return await _strategyEngine.BacktestStrategyAsync(
            StrategyEntity,
            request.StartDate,
            request.EndDate,
            request.InitialCapital
        );
    }
}


}
