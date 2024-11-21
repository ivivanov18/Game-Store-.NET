using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres = 
[
	new Genre {Id = new Guid("dd243b4a-71dc-4a0b-b11d-d67a55a8586d"), Name = "Fighting" },
	new Genre {Id = new Guid("9c9b8ff8-368a-423e-9d1c-6c4e950d2124"), Name = "Kids and Family" },
	new Genre {Id = new Guid("dcfc8948-d713-4d4d-a06b-d7a9890a1309"), Name = "Racing" },
	new Genre {Id = new Guid("557c159f-7d25-4b3a-b905-3288c60eb569"), Name = "Roleplaying" },
	new Genre {Id = new Guid("ade72145-eb5e-4e08-87d4-11ffe0ad0e16"), Name = "Sports" },
	new Genre {Id = new Guid("f4d9a6a8-2a73-4f13-9e01-e52567507b00"), Name = "Shooter"}
];

List<Game> games = 
[
	new Game
	{
		Id = new Guid("a893498d-2c71-40c9-81ce-37e3934499f6"),
		Name = "Street Figher II",
		Genre = genres[0],
		Price = 19.99m,
		ReleaseDate = new DateOnly(1992,7, 15),
		Description = "First ever worldwide fighting game"
	},
		new Game
	{
		Id = new Guid("5f63d854-6ab3-48ec-97f5-13e414aba63e"),
		Name = "Final Fantasy VII",
		Genre = genres[3],
		Price = 59.99m,
		ReleaseDate = new DateOnly(2010,9, 30),
		Description = "Classic in the genre"
	},
		new Game
	{
		Id = new Guid("c33f8bd1-8f59-47d6-9c66-3bdf119b46ad"),
		Name = "Little Big Adventure 2",
		Genre = genres[1],
		Price = 29.99m,
		ReleaseDate = new DateOnly(1998,7, 15),
		Description = "Unforgettable adventure"
	}
];

// GET /games
app.MapGet("/games", () => games.Select(game => new GameSummaryDto(
	game.Id,
	game.Name,
	game.Genre.Name,
	game.Price,
	game.ReleaseDate
)));

// GET /games/:id
app.MapGet("/games/{id}", (Guid id) =>
{
	Game? game = games.Find(g => g.Id == id);

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
	var genre = genres.Find(g => g.Id == gameDto.GenreId);

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

	games.Add(game);
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
	var existingGame = games.Find(g=> g.Id == id);

	if (existingGame is null)
	{
		return Results.NotFound();
	}

	var genre = genres.Find(g => g.Id == updateGame.GenreId);

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
	games.RemoveAll(g => g.Id == id);

	return Results.NoContent();
});

app.MapGet("/genres", () => genres.Select(genre => new GenreDto(genre.Id, genre.Name)));

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