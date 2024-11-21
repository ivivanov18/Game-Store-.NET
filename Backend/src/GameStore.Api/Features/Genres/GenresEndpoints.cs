using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres;

public static class GenresEndpoints
{
	public static void MapGenres(this IEndpointRouteBuilder app, GameStoreData data)
	{
		var group = app.MapGroup("/genres");

		group.MapGetGenres(data);
	}
}
