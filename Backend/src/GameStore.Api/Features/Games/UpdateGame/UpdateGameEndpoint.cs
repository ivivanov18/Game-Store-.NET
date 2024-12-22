using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
	public static void MapUpdateGame(this IEndpointRouteBuilder app)
	{
		app.MapPut("/{id}", async (Guid id, UpdateGameDto updateGame, GameStoreDbContext context) => 
		{
			var existingGame = await context.Games.FindAsync(id);

			if (existingGame is null)
			{
				return Results.NotFound();
			}

			existingGame.Name = updateGame.Name;
			existingGame.GenreId = updateGame.GenreId;
			existingGame.Price = updateGame.Price;
			existingGame.ReleaseDate = updateGame.ReleaseDate;
			existingGame.Description = updateGame.Description;

			await context.SaveChangesAsync();

			return Results.NoContent();
		})
		.WithParameterValidation();
	}
}
