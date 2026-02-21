using MediatR;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.Application.Handlers
{
    public record UpdateSignalCommand(Guid Id, string Type, DateTime Timestamp) : IRequest<Signal>;

    public class UpdateSignalHandler : IRequestHandler<UpdateSignalCommand, Signal>
    {
        public Task<Signal> Handle(UpdateSignalCommand request, CancellationToken cancellationToken)
        {
            var signal = new Signal(request.Id, request.Type, request.Timestamp);
            return Task.FromResult(signal);
        }
    }
}
