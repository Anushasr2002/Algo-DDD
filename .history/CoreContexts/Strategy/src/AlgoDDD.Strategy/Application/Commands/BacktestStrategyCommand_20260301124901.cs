using AlgoDDD.SharedKernel;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Domain;
using MediatR;
using AlgoDDD.Strategy.Domain.Services;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Application.Commands
{
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
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _strategyEngine = strategyEngine ?? throw new ArgumentNullException(nameof(strategyEngine));
        }

        public async Task<BacktestResult> Handle(BacktestStrategyCommand request, CancellationToken cancellationToken)
        {
            // ✅ Wrap string into StrategyId value object
            var strategyId = new StrategyId(request.StrategyId);

            var strategyEntity = await _repository.GetByIdAsync(strategyId);
            if (strategyEntity == null)
                throw new Exception($"StrategyEntity {request.StrategyId} not found");

            return await _strategyEngine.BacktestStrategyAsync(
                strategyEntity,
                request.StartDate,
                request.EndDate,
                request.InitialCapital
            );
        }
    }
}
