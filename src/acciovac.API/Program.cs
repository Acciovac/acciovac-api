using acciovac.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

// OpenAPI (new .NET OpenAPI)
builder.Services.AddOpenApi();

// Infrastructure (EF Core, DbContext)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// --------------------
// Pipeline
// --------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// --------------------
// Endpoints
// --------------------

app.MapGet("/health", () =>
    Results.Ok("AccioVac API is running 🚀")
);

app.Run();
