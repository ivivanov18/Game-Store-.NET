using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
	public static void MapUpdateGame(this IEndpointRouteBuilder app)
	{
		app.MapPut("/{id}", (Guid id, UpdateGameDto updateGame, GameStoreData data) => 
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
			existingGame.GenreId = updateGame.GenreId;
			existingGame.Price = updateGame.Price;
			existingGame.ReleaseDate = updateGame.ReleaseDate;

			return Results.NoContent();
		})
		.WithParameterValidation();
	}
}
