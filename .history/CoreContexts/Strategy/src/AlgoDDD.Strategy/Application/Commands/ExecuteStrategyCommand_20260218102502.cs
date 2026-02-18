using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain;
using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;

// Explicit aliasing to resolve ambiguity
using EntitySignal = AlgoDDD.Strategy.Domain.Entities.Signal;
using DomainSignal = AlgoDDD.Strategy.Domain.Signal;

namespace AlgoDDD.Strategy.Application.Commands
{
    // Command definition using MediatR
    public class ExecuteStrategyCommand : IRequest<List<EntitySignal>>
    {
        public string StrategyId { get; set; } = string.Empty;
    }

    // Command handler
    public class ExecuteStrategyCommandHandler : IRequestHandler<ExecuteStrategyCommand, List<EntitySignal>>
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

        public async Task<List<EntitySignal>> Handle(ExecuteStrategyCommand request, CancellationToken cancellationToken)
        {
            var strategyEntity = await _repository.GetByIdAsync(request.StrategyId);
            if (strategyEntity == null)
                throw new Exception($"StrategyEntity {request.StrategyId} not found");

            // Explicitly return EntitySignal list to avoid ambiguity
            return await _strategyEngine.ExecuteStrategyAsync(strategyEntity);
        }
    }
}
