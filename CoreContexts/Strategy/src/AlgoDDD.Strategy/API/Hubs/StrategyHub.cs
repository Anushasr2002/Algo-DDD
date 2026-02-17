using Microsoft.AspNetCore.SignalR;
using AlgoDDD.Strategy.Domain.Entities;

namespace AlgoDDD.Strategy.API.Hubs;

public class StrategyHub : Hub
{
    public async Task SubscribeToStrategy(string strategyId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"strategy-{strategyId}");
    }

    public async Task UnsubscribeFromStrategy(string strategyId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"strategy-{strategyId}");
    }

    public async Task SendSignal(Signal signal)
    {
        await Clients.Group($"strategy-{signal.Id}").SendAsync("SignalReceived", signal);
    }
}
