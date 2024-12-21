using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
	public static void MapDeleteGame(this IEndpointRouteBuilder app)
	{
		// DELETE /games/:id
		app.MapDelete("/{id}", (Guid id, GameStoreDbContext context) =>
		{
			context.Games
					.Where(g => g.Id == id)
					.ExecuteDelete();

			return Results.NoContent();
		});
	}
}
