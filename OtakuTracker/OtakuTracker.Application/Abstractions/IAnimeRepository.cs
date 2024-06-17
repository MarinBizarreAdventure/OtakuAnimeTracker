using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeRepository
    {
        Anime Create(Anime anime);
        Anime GetById(int animeId);
        List<Anime> GetAnimesByIds(List<int> animeIds);
        void Update(Anime anime);
        void Delete(int animeId);
        List<Anime> GetAll();
        List<Anime> GetByGenre(int genreId);
        List<Anime> GetByTheme(int themeId);
        List<Anime> GetByStatus(string status);
        List<Anime> Search(string keyword);
    }
}
