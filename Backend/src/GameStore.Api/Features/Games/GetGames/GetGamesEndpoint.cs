using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
	public static void MagGetGames(this IEndpointRouteBuilder app)
	{
		// GET /games
		app.MapGet("/", async ( GameStoreDbContext context, [AsParameters] GetGamesDto request) => {

			var skipCount = (request.PageNumber - 1) * request.PageSize;

			var filteredGames = context.Games
										.Where(game => string.IsNullOrWhiteSpace(request.Name) 
													|| EF.Functions.Like(game.Name, $"%{request.Name}%"));

			var gamesOnPage = await filteredGames
						.OrderBy(game => game.Name)
						.Skip(skipCount)
						.Take(request.PageSize)
						.Include(game => game.Genre)
						.Select(game => new GameSummaryDto(
							game.Id,
							game.Name,
							game.Genre!.Name,
							game.Price,
							game.ReleaseDate
						))
						.AsNoTracking()
						.ToListAsync();

			var totalGames = await filteredGames.CountAsync();
			var totalPages = (int)Math.Ceiling(totalGames / (double)request.PageSize);

			return new GamesPageDto(totalPages, gamesOnPage);
		});
	}
}
