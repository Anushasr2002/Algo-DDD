using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlgoDDD.Strategy.Domain.Entities;
using AlgoDDD.Strategy.Domain.Interfaces;   // ✅ Now resolves IStrategyExecutor
using AlgoDDD.Strategy.Domain.ValueObjects; // ✅ For StrategyId

namespace AlgoDDD.Strategy.Application.Commands
{
    public class ExecuteStrategyCommand : IRequest<bool>
    {
        public StrategyId StrategyId { get; init; }

        public ExecuteStrategyCommand(StrategyId strategyId)
        {
            StrategyId = strategyId;
        }
    }

    public class ExecuteStrategyCommandHandler : IRequestHandler<ExecuteStrategyCommand, bool>
    {
        private readonly IStrategyExecutor _executor;

        public ExecuteStrategyCommandHandler(IStrategyExecutor executor)
        {
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public async Task<bool> Handle(ExecuteStrategyCommand request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync(request.StrategyId, cancellationToken);
        }
    }
}
