using Microsoft.AspNetCore.SignalR;

namespace AlgoDDD.Api.Hubs
{
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
    }
}
