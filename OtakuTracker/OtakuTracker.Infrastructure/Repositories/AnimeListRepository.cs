using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

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

        public async Task<List<AnimeList>> GetUserListByUserId(string username)
        {
            return await _context.AnimeLists.Where(al => al.Username == username).ToListAsync();
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