using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Genres;

public static class GetGenres
{
	public static void MapGetGenres(this IEndpointRouteBuilder app)
	{
		app.MapGet("/", 
			async (GameStoreDbContext context) =>
				await context.Genres
						.Select(genre => new GenreDto(genre.Id, genre.Name))
						.AsNoTracking()
						.ToListAsync());
	}
}
