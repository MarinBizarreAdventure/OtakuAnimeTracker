using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeRepository
    {
        Task<Anime> Create(Anime anime);
        Task<Anime> GetById(int animeId);
        Task Update(Anime anime);
        Task<bool> Delete(int animeId);
        
        Task<List<int>> GetPopularAnimeIds(int page, int pageSize, string sortOrder);
        Task<List<int>> GetRankedAnimeIds(int page, int pageSize, string sortOrder);
        Task<AnimeSummaryDto> GetAnimeSummaryById(int animeId);
        Task<int> CountAnimes();
    }
}
