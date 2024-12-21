using GameStore.Api.Data;
using GameStore.Api.Exceptions;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

// Register DbContext
builder.Services.AddSqlite<GameStoreDbContext>(connString);

// REGISTER SERVICES
builder.Services.AddTransient<GameDataLogger>();
builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();
app.InitializeDb();
app.AddCustomExceptionHandling();
app.Run();
