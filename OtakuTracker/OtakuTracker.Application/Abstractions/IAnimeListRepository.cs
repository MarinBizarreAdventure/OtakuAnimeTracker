using OtakuTracker.Application.AnimeLists.Responses;
using OtakuTracker.Domain.Enumerations;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeListRepository
    {
        Task<AnimeList> CreateUserList(AnimeList animeList);
        Task<List<AnimeListDto>> GetUserListByUserId(string username);
        Task<AnimeList> AddAnimeToUserList(AnimeList animeList);
        Task RemoveAnimeFromUserList(string username, int animeId);

        
        // Task UpdateAnimeStatus(string username, int animeId, int watchingStatus);
        // Task UpdateAnimeScore(string username, int animeId, int? score);
        // Task UpdateWatchedEpisodes(string username, int animeId, int? watchedEpisodes);
    }
}
