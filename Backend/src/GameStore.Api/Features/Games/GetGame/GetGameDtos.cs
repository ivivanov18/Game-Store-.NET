namespace GameStore.Api.Features.Games.GetGame;

public record GameDetailDto(
	Guid Id,
	string Name,
	Guid GenreId,
	decimal Price,
	DateOnly ReleaseDate,
	string Description
);