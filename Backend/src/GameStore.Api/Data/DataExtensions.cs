using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
	private static void MigrateDb(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		GameStoreDbContext? context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
		context.Database.Migrate();
	}

	private static void SeedDb(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		GameStoreDbContext? context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();

		if (!context.Genres.Any())
		{
			context.Genres.AddRange(
				new Genre {Name = "Fighting"},
				new Genre {Name = "Racing"},
				new Genre {Name = "Kids and Playing"},
				new Genre {Name = "Roleplaying"},
				new Genre {Name = "Sports"}
			);

			context.SaveChanges();
		}
	}

	public static void InitializeDb(this WebApplication app)
	{
		app.MigrateDb();
		app.SeedDb();
	}
}
