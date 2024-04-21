namespace SignalRExample.Api.Model
{
    public class OnMessageConnectionIdRecievedRequest
    {
        public string Message { get; init; } = string.Empty;
        public string ConnectionId { get; init; } = string.Empty;
    }
}
