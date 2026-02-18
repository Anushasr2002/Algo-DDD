using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.Aggregates;
using AlgoDDD.Strategy.Domain.ValueObjects;
using MediatR;

namespace AlgoDDD.Strategy.Application.Commands
{
    /// <summary>
    /// Command to create a new trading strategy.
    /// </summary>
    public class CreateStrategyCommand : IRequest<StrategyEntity>
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

    /// <summary>
    /// Handles creation of a new strategy and persists it via repository.
    /// </summary>
    public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, StrategyEntity>
    {
        private readonly IStrategyRepository _repository;

        public CreateStrategyCommandHandler(IStrategyRepository repository)
        {
            _repository = repository;
        }

        public async Task<StrategyEntity> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
        {
            var strategyEntity = new StrategyEntity(
                Guid.NewGuid(),
                request.Name,
                request.Type,
                request.Description,
                request.Parameters,
                request.Symbols,
                request.TimeFrame,
                request.MaxPositionSize,
                request.StopLoss,
                request.TakeProfit
            );

            return await _repository.AddAsync(strategyEntity);
        }
    }
}
