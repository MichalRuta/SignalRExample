using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Api.SignalR
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.OnMessageRecieved($"Attention! {Context.ConnectionId} listens to what's going on ;)");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.OnMessageRecieved($"{Context.ConnectionId}: {message}");
        }
    }
}
