namespace SignalRExample.Api.SignalR
{
    public interface IChatClient
    {
        Task OnMessageRecieved(string message);
    }
}
