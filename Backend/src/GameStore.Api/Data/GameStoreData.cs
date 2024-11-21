using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
	private readonly List<Genre> genres = 
	[
		new Genre {Id = new Guid("dd243b4a-71dc-4a0b-b11d-d67a55a8586d"), Name = "Fighting" },
		new Genre {Id = new Guid("9c9b8ff8-368a-423e-9d1c-6c4e950d2124"), Name = "Kids and Family" },
		new Genre {Id = new Guid("dcfc8948-d713-4d4d-a06b-d7a9890a1309"), Name = "Racing" },
		new Genre {Id = new Guid("557c159f-7d25-4b3a-b905-3288c60eb569"), Name = "Roleplaying" },
		new Genre {Id = new Guid("ade72145-eb5e-4e08-87d4-11ffe0ad0e16"), Name = "Sports" },
		new Genre {Id = new Guid("f4d9a6a8-2a73-4f13-9e01-e52567507b00"), Name = "Shooter"}
	];

	private readonly List<Game> games;

	public GameStoreData()
	{
		games = 
		[
			new Game
			{
				Id = new Guid("a893498d-2c71-40c9-81ce-37e3934499f6"),
				Name = "Street Figher II",
				Genre = genres[0],
				Price = 19.99m,
				ReleaseDate = new DateOnly(1992,7, 15),
				Description = "First ever worldwide fighting game"
			},
				new Game
			{
				Id = new Guid("5f63d854-6ab3-48ec-97f5-13e414aba63e"),
				Name = "Final Fantasy VII",
				Genre = genres[3],
				Price = 59.99m,
				ReleaseDate = new DateOnly(2010,9, 30),
				Description = "Classic in the genre"
			},
				new Game
			{
				Id = new Guid("c33f8bd1-8f59-47d6-9c66-3bdf119b46ad"),
				Name = "Little Big Adventure 2",
				Genre = genres[1],
				Price = 29.99m,
				ReleaseDate = new DateOnly(1998,7, 15),
				Description = "Unforgettable adventure"
			}
		];
	}

	public IEnumerable<Game> GetGames() => games;

	public Game? GetGame(Guid id) => games.Find(g => g.Id == id);

	public void RemoveGame(Guid id) => games.RemoveAll(g => g.Id == id);

	public void AddGame(Game game)
	{
		game.Id = Guid.NewGuid();
		games.Add(game);
	}

	public IEnumerable<Genre> GetGenres() => genres;

	public Genre? GetGenre(Guid id) => genres.Find(g => g.Id == id);
}
