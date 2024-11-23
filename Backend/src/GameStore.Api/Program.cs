using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.DeleteGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

// REGISTER SERVICES
builder.Services.AddTransient<GameStoreData>();

var app = builder.Build();

app.MapGames();
app.MapGenres();
app.Run();
