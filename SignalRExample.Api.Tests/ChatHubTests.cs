using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SignalRExample.Api.Model;
using SignalRExample.Api.SignalR;

namespace SignalRExample.Api.Tests
{
    public class ChatHubTests
    {
        private readonly HubConnection _connection;
        public ChatHubTests()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSignalR();
                })
                .Configure(app =>
                {
                    app
                    .UseRouting()
                    .UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chatHub");
                    });
                });

            var server = new TestServer(webHostBuilder);

            this._connection = new HubConnectionBuilder()
                .WithUrl("http://localhost/chatHub",
                    o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();

        }

        [Fact]
        public async Task GivenMessageSent_WhenMessageRecieved_ThenContainSentMessage()
        {
            var message = "Integration Testing in Microsoft AspNetCore SignalR";
            var echo = string.Empty;
            _connection.On<string>("OnMessageRecieved", msg =>
            {
                echo = msg;
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendMessage", message);

            echo.Should().Contain(message);
        }

        [Fact]
        public async Task GivenMessageSentConnectionId_WhenMessageRecieved_ThenContainSentMessage()
        {
            var message = "Integration Testing in Microsoft AspNetCore SignalR";
            var echo = new OnMessageConnectionIdRecievedRequest();
            _connection.On<OnMessageConnectionIdRecievedRequest>("OnDirectMessageReceived", msg =>
            {
                echo = msg;

            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendDirectMessage", 
                new OnMessageConnectionIdRecievedRequest(){
                    Message = message,
                    ConnectionId = _connection.ConnectionId!});

            echo.Message.Should().Contain(message);
        }

        [Fact]
        public async Task GivenMessageSentWrongConnectionId_WhenMessageRecieved_ThenNOTContainSentMessage()
        {
            var message = "Integration Testing in Microsoft AspNetCore SignalR";
            var connectionId = Guid.NewGuid().ToString();
            var echo = new OnMessageConnectionIdRecievedRequest();
            _connection.On<OnMessageConnectionIdRecievedRequest>("OnDirectMessageReceived", msg =>
            {
                echo = msg;

            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("SendDirectMessage",
                new OnMessageConnectionIdRecievedRequest()
                {
                    Message = message,
                    ConnectionId = connectionId
                });

            echo.Message.Should().BeEmpty();
        }
    }
}