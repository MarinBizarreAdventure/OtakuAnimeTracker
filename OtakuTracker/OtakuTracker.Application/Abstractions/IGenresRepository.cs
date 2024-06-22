using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IGenresRepository
    {
        // Task<Genre> CreateGenres(Genre genres);
        Task<Genre> GetGenresById(int genresId);
        Task<List<Genre>> GetAllGenres();
        // Task<Genre> UpdateGenres(Genre genres);
        // Task DeleteGenres(int genresId);
    }
}
