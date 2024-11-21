using System.ComponentModel.DataAnnotations;
using GameStore.Api.Data;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

GameStoreData data = new();

// GET /games
app.MapGet("/games", () => data.GetGames().Select(game => new GameSummaryDto(
	game.Id,
	game.Name,
	game.Genre.Name,
	game.Price,
	game.ReleaseDate
)));

// GET /games/:id
app.MapGet("/games/{id}", (Guid id) =>
{
	Game? game = data.GetGame(id);

	return game is null ? Results.NotFound() : Results.Ok(
		new GameDetailDto(
			game.Id,
			game.Name,
			game.Genre.Id,
			game.Price,
			game.ReleaseDate,
			game.Description
		)
	);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto gameDto) =>
{
	var genre = data.GetGenre(gameDto.GenreId);

	if (genre is null)
	{
		return Results.BadRequest("Invalid Genre Id");
	}

	var game = new Game
	{
		Id = Guid.NewGuid(),
		Name = gameDto.Name,
		Genre = genre,
		Price = gameDto.Price,
		ReleaseDate = gameDto.ReleaseDate,
		Description = gameDto.Description
	};

	data.AddGame(game);
	return Results.CreatedAtRoute(
		GetGameEndpointName,
		new {id = game.Id},
		new GameDetailDto(
			game.Id,
			game.Name,
			game.Genre.Id,
			game.Price,
			game.ReleaseDate,
			game.Description
		)
	);
})
.WithParameterValidation();

app.MapPut("/games/{id}", (Guid id, UpdateGameDto updateGame) => 
{
	var existingGame = data.GetGame(id);

	if (existingGame is null)
	{
		return Results.NotFound();
	}

	var genre = data.GetGenre(updateGame.GenreId);

	if (genre is null)
	{
		return Results.BadRequest("Invalid Genre Id");
	}
	existingGame.Name = updateGame.Name;
	existingGame.Genre = genre;
	existingGame.Price = updateGame.Price;
	existingGame.ReleaseDate = updateGame.ReleaseDate;

	return Results.NoContent();
})
.WithParameterValidation();

// DELETE /games/:id
app.MapDelete("/games/{id}", (Guid id) =>
{
	data.RemoveGame(id);

	return Results.NoContent();
});

app.MapGet("/genres", () => data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailDto(
	Guid Id,
	string Name,
	Guid GenreId,
	decimal Price,
	DateOnly ReleaseDate,
	string Description
);

public record GameSummaryDto(
	Guid Id,
	string Name,
	string Genre,
	decimal Price,
	DateOnly ReleaseDate
);

public record CreateGameDto(
	[Required]
	[StringLength(50)]
	string Name,
	Guid GenreId,
	[Range(1,100)]
	decimal Price,
	DateOnly ReleaseDate,
	[Required]
	[StringLength(500)]
	string Description
);

public record UpdateGameDto(
	[Required]
	[StringLength(50)]
	string Name,
	Guid GenreId,
	[Range(1,100)]
	decimal Price,
	DateOnly ReleaseDate,
	[Required]
	[StringLength(500)]
	string Description
);
public record GenreDto(
	Guid Id,
	string Name
);