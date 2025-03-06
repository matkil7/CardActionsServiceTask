using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Providers.Actions;
using CardActionsApi.Services;
using Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddSingleton<ISpecificationsProvider<CardDetails>, ActionsSpecificationsProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/actions/{cardNumber}/{userId}", async (string cardNumber, string userId, IActionService actionService) =>
    {
        var actions = await actionService.GetCardActions(userId, cardNumber);

        return actions.Match(onSuccess: (x) => Results.Json(x),
            onFailure: (errors) => Results.NotFound());
    })
    .WithName("GetCardActions")
    .WithOpenApi();

app.Run();
