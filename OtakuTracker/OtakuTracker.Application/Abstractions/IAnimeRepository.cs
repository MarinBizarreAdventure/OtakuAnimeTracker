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
        
        Task<List<AnimeSummaryDto>> GetTopAnimes(int page, int pageSize);
        Task<List<AnimeSummaryDto>> GetUpcomingAnimes(int page, int pageSize);
        Task<List<AnimeSummaryDto>> GetAiringAnimes(int page, int pageSize);
    }
}
