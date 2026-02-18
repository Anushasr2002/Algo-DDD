using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Services;
using MediatR;

namespace AlgoDDD.Strategy.Application.Commands
{
    /// <summary>
    /// Command to execute a strategy by its ID.
    /// Returns a list of generated signals.
    /// </summary>
    public class ExecuteStrategyCommand : IRequest<List<Signal>>
    {
        public Guid StrategyId { get; }

        public ExecuteStrategyCommand(Guid strategyId)
        {
            StrategyId = strategyId;
        }
    }

    /// <summary>
    /// Handles execution of a strategy using the StrategyEngine.
    /// </summary>
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
            var strategyEntity = await _repository.GetByIdAsync(request.StrategyId);
            if (strategyEntity == null)
                throw new Exception($"StrategyEntity {request.StrategyId} not found");

            return await _strategyEngine.ExecuteStrategyAsync(strategyEntity);
        }
    }
}
