using MediatR;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Records;

public record UpdateAnime(
    int AnimeId,
    string? Name,
    string? EnglishName,
    string? JapaneseName,
    string? ImageUrl,
    string? Type,
    int? Episodes,
    string? Aired,
    string? Premiered,
    string? Producers,
    string? Licensors,
    string? Studios,
    string? Source,
    string? Duration,
    string? Synopsis,
    string? Rating,
    int? Ranked,
    int? Popularity,
    int? Members,
    int? Favorites,
    int? Watching,
    int? Completed,
    int? OnHold,
    int? Dropped,
    int? PlanToWatch,
    int? Score10,
    int? Score9,
    int? Score8,
    int? Score7,
    int? Score6,
    int? Score5,
    int? Score4,
    int? Score3,
    int? Score2,
    int? Score1,
    List<GenreDto>? Genres
) : IRequest<AnimeDto>;