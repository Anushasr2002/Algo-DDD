using AlgoDDD.Strategy.Application.Commands;
using AlgoDDD.Strategy.Domain.Interfaces;
using AlgoDDD.Strategy.Infrastructure.Repositories;
using AlgoDDD.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR registration (Application layer)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateStrategyCommand).Assembly));

// Repository registration (Domain + Infrastructure)
builder.Services.AddScoped<IStrategyRepository, StrategyRepository>();

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<StrategyHub>("/strategyHub");

app.Run();
