using OtakuTracker.Domain.Enumerations;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeListRepository
    {
        Task<AnimeList> CreateUserList(int userId);
        Task<AnimeList> GetUserListByUserId(int userId);
        Task<AnimeList> AddAnimeToUserList(int userId, int animeId, AnimeStatus status, decimal score);
        Task UpdateAnimeStatus(int userId, int animeId, AnimeStatus status);
        Task UpdateAnimeScore(int userId, int animeId, decimal score);
        Task RemoveAnimeFromUserList(int userId, int animeId);
    }
}
