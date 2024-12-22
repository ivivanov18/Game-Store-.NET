using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
	private static async Task MigrateDbAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		GameStoreDbContext? context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
		await context.Database.MigrateAsync();
	}

	private static async Task SeedDbAsync(this WebApplication app)
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

			await context.SaveChangesAsync();
		}
	}

	public static async Task InitializeDbAsync(this WebApplication app)
	{
		await app.MigrateDbAsync();
		await app.SeedDbAsync();

		app.Logger.LogInformation("Database is initialized");
	}
}
