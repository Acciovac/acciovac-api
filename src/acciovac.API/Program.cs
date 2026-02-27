using acciovac.API.Middleware;
using acciovac.Application;
using acciovac.Application.Behaviors.Users.Commands.CreateUser;
using acciovac.Application.Common.ValidationBehaviors;
using acciovac.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Registered here...

// Infrastructure (EF Core, DbContext)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// --------------------
// Pipeline
// --------------------

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

// --------------------
// Endpoints
// --------------------

app.MapGet("/health", () =>
    Results.Ok("AccioVac API is running 🚀")
);

app.Run();