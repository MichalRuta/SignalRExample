using SignalRExample.Api.Model;

namespace SignalRExample.Api.SignalR
{
    public interface IChatClient
    {
        Task OnMessageRecieved(string message);
        Task OnDirectMessageReceived(OnMessageConnectionIdRecievedRequest request);
    }
}
