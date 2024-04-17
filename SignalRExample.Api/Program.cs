using Microsoft.AspNetCore.SignalR;
using SignalRExample.Api.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("message", async (string inputText, IHubContext<ChatHub, IChatClient> context) =>
{
    await context.Clients.All.ReceiveMessage(inputText);
    return Results.Accepted();
});

app.UseHttpsRedirection();

app.MapHub<ChatHub>("chatHub");

app.Run();
