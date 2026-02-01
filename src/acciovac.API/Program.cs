using AccioVac.Infrastructure;
using AccioVac.Application;
using AccioVac.Infrastructure.Persistence;
using MediatR;
using AccioVac.Application.Trips.Commands;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Layers
builder.Services.AddApplicationServices(); // Extension method to register MediatR
builder.Services.AddInfrastructureServices(builder.Configuration); // Registers EF Core

var app = builder.Build();

// 2. Create Database automatically (Good for Dev)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Creates DB and Seeds the Dev User
}

// 3. API Endpoints
app.MapPost("/api/trips", async (IMediator mediator, CreateTripCommand command) =>
{
    var tripId = await mediator.Send(command);
    return Results.Created($"/api/trips/{tripId}", new { Id = tripId });
});

app.Run();