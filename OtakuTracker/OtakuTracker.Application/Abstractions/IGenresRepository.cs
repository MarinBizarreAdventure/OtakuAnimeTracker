using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IGenresRepository
    {
        Genre CreateGenres(Genre genres);

        Genre GetGenresById(int genresId);

        List<Genre> GetAllGenres();

        Genre UpdateGenres(Genre genres);

        void DeleteGenres(int genresId);

    }
}
