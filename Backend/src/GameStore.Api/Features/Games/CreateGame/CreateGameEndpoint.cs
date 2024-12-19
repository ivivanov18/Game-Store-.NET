using System;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
	public static void MapCreateGame(this IEndpointRouteBuilder app)
	{
		// POST /games
		app.MapPost("/", (CreateGameDto gameDto, GameStoreDbContext context) =>
		{
			var game = new Game
			{
				Id = Guid.NewGuid(),
				Name = gameDto.Name,
				GenreId = gameDto.GenreId,
				Price = gameDto.Price,
				ReleaseDate = gameDto.ReleaseDate,
				Description = gameDto.Description
			};

			context.Games.Add(game);

			context.SaveChanges();

			return Results.CreatedAtRoute(
				EndpointNames.GetGame,
				new {id = game.Id},
				new GameDetailDto(
					game.Id,
					game.Name,
					game.GenreId,
					game.Price,
					game.ReleaseDate,
					game.Description
				)
			);
		})
		.WithParameterValidation();

	}

}
