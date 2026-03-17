using acciovac.API.Common;
using acciovac.API.Middleware;
using acciovac.Application;
using acciovac.Application.Abstractions; // Added for IItineraryPlanner 
using acciovac.Application.Behaviors.Users.Commands.CreateUser; 
using acciovac.Application.Common.ValidationBehaviors;
using acciovac.Infrastructure;
using acciovac.Infrastructure.Configuration; // Added for GeminiOptions                                 
using acciovac.Infrastructure.Services; // Added for GeminiItineraryPlanner 
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
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// --- ADDED: Gemini AI Configuration & Service ---
// Binds the settings from appsettings.json or Secret Manager
builder.Services.Configure<AiOptions>(builder.Configuration.GetSection(AiOptions.SectionName));

// Registers the AI Service
builder.Services.AddScoped<IItineraryPlanner, GeminiItineraryPlanner>();
// ------------------------------------------------

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

app.MapGet("/health", () => Results.Ok(ApiResponse.Success(new { message = "AccioVac API is running 🚀" }))
);

app.Run();