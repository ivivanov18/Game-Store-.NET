using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
	public static void MapDeleteGame(this IEndpointRouteBuilder app)
	{
		// DELETE /games/:id
		app.MapDelete("/{id}", async (Guid id, GameStoreDbContext context) =>
		{
			await context.Games
					.Where(g => g.Id == id)
					.ExecuteDeleteAsync();

			return Results.NoContent();
		});
	}
}
