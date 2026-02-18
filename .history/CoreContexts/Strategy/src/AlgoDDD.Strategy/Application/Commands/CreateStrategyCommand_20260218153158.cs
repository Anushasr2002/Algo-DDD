// Explicit alias to resolve ambiguity
using StrategyType = AlgoDDD.Strategy.Domain.ValueObjects.StrategyType;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.ValueObjects;
using MediatR;

namespace AlgoDDD.Strategy.Application.Commands
{
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
                request.Type,          // ✅ Pass value object directly
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
