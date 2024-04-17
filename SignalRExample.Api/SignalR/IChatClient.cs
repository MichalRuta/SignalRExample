namespace SignalRExample.Api.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
