using GameStore.Api.Data;
using GameStore.Api.Exceptions;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Middlewares;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

// Register DbContext
builder.Services.AddSqlite<GameStoreDbContext>(connString);

// REGISTER SERVICES
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddSingleton<GameStoreData>();

builder.Services.AddHttpLogging(options =>
{
	options.LoggingFields = HttpLoggingFields.RequestMethod |
							HttpLoggingFields.RequestPath |
							HttpLoggingFields.ResponseStatusCode |
							HttpLoggingFields.Duration;
	options.CombineLogs = true;
});

var app = builder.Build();

app.UseHttpLogging();
app.MapGames();
app.MapGenres();
await app.InitializeDbAsync();
app.AddCustomExceptionHandling();
app.Run();
