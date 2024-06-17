using OtakuTracker.Domain.Enumerations;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Abstractions
{
    public interface IAnimeListRepository
    {
        AnimeList CreateUserList(int userId);
        AnimeList GetUserListByUserId(int userId);
        AnimeList AddAnimeToUserList(int userId, int animeId, AnimeStatus status, decimal score);
        void UpdateAnimeStatus(int userId, int animeId, AnimeStatus status);
        void UpdateAnimeScore(int userId, int animeId, decimal score);
        void RemoveAnimeFromUserList(int userId, int animeId);
    }
}
