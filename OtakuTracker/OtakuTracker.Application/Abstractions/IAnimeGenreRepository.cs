using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions;

public interface IAnimeGenreRepository
{
    Task AddRange(List<AnimeGenre> animeGenres); 

    Task<AnimeGenre> Add(AnimeGenre animeGenre);
    Task<AnimeGenre> GetById(int animeGenreId);
    Task Update(AnimeGenre animeGenre);
    Task<bool> Delete(int animeGenreId);

    Task<List<int>> GetAllGenreIdsByAnimeId(int animeId);
    Task<List<int>> GetAllAnimeIdsByGenreId(int genreId, int page, int pageSize);
}