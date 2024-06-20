using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeRepository
    {
        Task<Anime> Create(Anime anime);
        Task<Anime> GetById(int animeId);
        // Task<List<Anime>> GetAnimesByIds(List<int> animeIds);
        Task Update(Anime anime);
        Task Delete(int animeId);
        // Task<List<Anime>> GetAll();
        // Task<List<Anime>> GetByGenre(int genreId);
        // Task<List<Anime>> Search(string keyword);
    }
}
