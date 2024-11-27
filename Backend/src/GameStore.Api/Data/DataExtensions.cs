using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
	public static void MigrateDb(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		GameStoreDbContext? context = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
		context.Database.Migrate();
	}

}
