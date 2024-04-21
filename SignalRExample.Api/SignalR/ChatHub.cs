using Microsoft.AspNetCore.SignalR;
using SignalRExample.Api.Model;

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

        public async Task SendDirectMessage(OnMessageConnectionIdRecievedRequest request)
        {
            await Clients.Client(request.ConnectionId).OnDirectMessageReceived(request);
        }
    }
}
