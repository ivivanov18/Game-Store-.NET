using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Shared.ExceptionHandling;
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

builder.Services.AddProblemDetails()
				.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseHttpLogging();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
}

app.UseStatusCodePages();
app.MapGames();
app.MapGenres();
await app.InitializeDbAsync();
app.Run();
