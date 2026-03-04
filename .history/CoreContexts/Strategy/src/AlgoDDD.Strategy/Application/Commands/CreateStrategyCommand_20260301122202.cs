using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;

// Explicit alias to resolve ambiguity
using StrategyType = AlgoDDD.Strategy.Domain.ValueObjects.StrategyType;

namespace AlgoDDD.Strategy.Application.Commands
{
    /// <summary>
    /// Command to create a new trading strategy.
    /// </summary>
    public class CreateStrategyCommand : IRequest<StrategyEntity>
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public StrategyType Type { get; init; }
        public Dictionary<string, object> Parameters { get; init; } = new();
        public List<string> Symbols { get; init; } = new();
        public TimeFrame TimeFrame { get; init; }
        public decimal? MaxPositionSize { get; init; }
        public decimal? StopLoss { get; init; }
        public decimal? TakeProfit { get; init; }
    }

    /// <summary>
    /// Handler for CreateStrategyCommand. Persists the strategy entity via repository.
    /// </summary>
    public class CreateStrategyCommandHandler : IRequestHandler<CreateStrategyCommand, StrategyEntity>
    {
        private readonly IStrategyRepository _repository;

        public CreateStrategyCommandHandler(IStrategyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<StrategyEntity> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
{
    // Align with StrategyEntity constructor (id, type, symbols)
    var strategyEntity = new StrategyEntity(
        new StrategyId(Guid.NewGuid()),   // Generate or wrap an ID
        request.Type,
        request.Symbols
    );

    // Persist via repository
    return await _repository.AddAsync(strategyEntity);
}

        }
    }
}
