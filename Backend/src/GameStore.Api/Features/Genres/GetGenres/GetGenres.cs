using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres;

public static class GetGenres
{
	public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
	{
		app.MapGet("/", () => data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name)));
	}

}
