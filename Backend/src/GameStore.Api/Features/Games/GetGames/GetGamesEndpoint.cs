using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
	public static void MagGetGames(this IEndpointRouteBuilder app)
	{
		// GET /games
		app.MapGet("/", async ( GameStoreDbContext context) => 
			await context.Games
				.Include(game => game.Genre)
				.Select(game => new GameSummaryDto(
					game.Id,
					game.Name,
					game.Genre!.Name,
					game.Price,
					game.ReleaseDate
				))
				.AsNoTracking()
				.ToListAsync());
	}
}
