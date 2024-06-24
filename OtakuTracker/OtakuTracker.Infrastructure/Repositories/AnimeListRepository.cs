using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.AnimeLists.Responses;
using AnimeList = OtakuTracker.Domain.Models.AnimeList;

namespace OtakuTracker.Infrastructure.Repositories;

public class AnimeListRepository : IAnimeListRepository{
        private readonly OtakutrackerContext _context;

        public AnimeListRepository(OtakutrackerContext context)
        {
            _context = context;
        }

        public async Task<AnimeList> CreateUserList(AnimeList animeList)
        {
            _context.AnimeLists.Add(animeList);
            await _context.SaveChangesAsync();
            return animeList;
        }

        public async Task<List<AnimeListDto>> GetUserListByUserId(string username)
        {
            return await _context.AnimeLists
                .Where(al => al.Username == username)
                .Select(al => new AnimeListDto
                {
                    Username = al.Username,
                    AnimeId = al.AnimeId,
                    Title = al.Anime.Name, // Getting the title from the Anime table
                    ImageUrl = al.Anime.ImageUrl, // Getting the image URL from the Anime table
                    Score = al.Score,
                    WatchingStatus = al.WatchingStatus,
                    WatchedEpisodes = al.WatchedEpisodes,
                    MyStartDate = al.MyStartDate,
                    MyFinishDate = al.MyFinishDate,
                    MyRewatching = al.MyRewatching,
                    MyRewatchingEp = al.MyRewatchingEp,
                    MyLastUpdated = al.MyLastUpdated,
                    MyTags = al.MyTags
                }).ToListAsync();
        }

        public async Task<AnimeList> AddAnimeToUserList(AnimeList animeList)
        {
            _context.AnimeLists.Add(animeList);
            await _context.SaveChangesAsync();
            return animeList;
        }

        public async Task RemoveAnimeFromUserList(string username, int animeId)
        {
            var animeList = await _context.AnimeLists.FindAsync(username, animeId);
            if (animeList != null)
            {
                _context.AnimeLists.Remove(animeList);
                await _context.SaveChangesAsync();
            }
        }
    }