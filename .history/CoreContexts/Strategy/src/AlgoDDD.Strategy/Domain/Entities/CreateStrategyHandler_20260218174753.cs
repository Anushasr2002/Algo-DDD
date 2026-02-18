using MediatR;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Application.Commands
{
    public record CreateStrategyCommand(Guid Id, string Name) : IRequest<StrategyEntity>;

    public class CreateStrategyHandler : IRequestHandler<CreateStrategyCommand, StrategyEntity>
    {
        public Task<StrategyEntity> Handle(CreateStrategyCommand request, CancellationToken cancellationToken)
        {
            var strategy = new StrategyEntity(request.Id, request.Name);
            return Task.FromResult(strategy);
        }
    }
}
