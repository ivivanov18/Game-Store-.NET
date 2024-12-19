using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
	public static void MapUpdateGame(this IEndpointRouteBuilder app)
	{
		app.MapPut("/{id}", (Guid id, UpdateGameDto updateGame, GameStoreDbContext context) => 
		{
			var existingGame = context.Games.Find(id);

			if (existingGame is null)
			{
				return Results.NotFound();
			}

			existingGame.Name = updateGame.Name;
			existingGame.GenreId = updateGame.GenreId;
			existingGame.Price = updateGame.Price;
			existingGame.ReleaseDate = updateGame.ReleaseDate;
			existingGame.Description = updateGame.Description;

			context.SaveChanges();

			return Results.NoContent();
		})
		.WithParameterValidation();
	}
}
