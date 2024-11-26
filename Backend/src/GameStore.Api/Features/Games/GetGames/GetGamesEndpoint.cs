using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
	public static void MagGetGames(this IEndpointRouteBuilder app)
	{
		// GET /games
		app.MapGet("/", ( GameStoreData data) => data.GetGames().Select(game => new GameSummaryDto(
			game.Id,
			game.Name,
			game.Genre!.Name,
			game.Price,
			game.ReleaseDate
		)));
	}
}
