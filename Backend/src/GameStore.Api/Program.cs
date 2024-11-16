using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Game> games = 
[
	new Game
	{
		Id = Guid.NewGuid(),
		Name = "Street Figher II",
		Genre = "Fighting",
		Price = 19.99m,
		ReleaseDate = new DateOnly(1992,7, 15)
	},
		new Game
	{
		Id = Guid.NewGuid(),
		Name = "Final Fantasy VII",
		Genre = "Roleplaying",
		Price = 59.99m,
		ReleaseDate = new DateOnly(2010,9, 30)
	},
		new Game
	{
		Id = Guid.NewGuid(),
		Name = "Little Big Adventure 2",
		Genre = "Adventure",
		Price = 29.99m,
		ReleaseDate = new DateOnly(1998,7, 15)
	}
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/:id
app.MapGet("/games/{id}", (Guid id) =>
{
	Game? game = games.Find(g => g.Id == id);

	return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (Game game) =>
{
	game.Id = Guid.NewGuid();
	games.Add(game);
	return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
})
.WithParameterValidation();

app.MapPut("/games/{id}", (Guid id, Game updateGame) => 
{
	var existingGame = games.Find(g=> g.Id == id);

	if (existingGame is null)
	{
		return Results.NotFound();
	}

	existingGame.Name = updateGame.Name;
	existingGame.Genre = updateGame.Genre;
	existingGame.Price = updateGame.Price;
	existingGame.ReleaseDate = updateGame.ReleaseDate;

	return Results.NoContent();
})
.WithParameterValidation();

// DELETE /games/:id
app.MapDelete("/games/{id}", (Guid id) =>
{
	games.RemoveAll(g => g.Id == id);

	return Results.NoContent();
});

app.Run();
