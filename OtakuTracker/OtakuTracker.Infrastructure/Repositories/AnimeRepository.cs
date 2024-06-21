using OtakuTracker.Application.Abstractions;
using Anime = OtakuTracker.Domain.Models.Anime;

namespace OtakuTracker.Infrastructure.Repositories;

 public class AnimeRepository : IAnimeRepository {
     
        private readonly OtakutrackerContext _context;

        public AnimeRepository(OtakutrackerContext context)
        {
            _context = context;
        }

        public async Task<Anime> Create(Anime anime)
        {
            
            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
            return anime;
        }

        public async Task<Anime> GetById(int animeId)
        {
            return await _context.Animes.FindAsync(animeId);
        }
        
        public async Task Update(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int animeId)
        {
            var anime = await _context.Animes.FindAsync(animeId);
            
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // public async Task<List<Anime>> GetAnimesByIds(List<int> animeIds)
        // {
        //     return await _context.Animes.Where(a => animeIds.Contains(a.Id)).ToListAsync();
        // }

       

        // public async Task<List<Anime>> GetAll()
        // {
        //     return await _context.Animes.ToListAsync();
        // }

        // public async Task<List<Anime>> GetByGenreAsync(int genreId)
        // {
        //     return await _context.Animes.Where(a => a.Genres.Any(g => g.GenreId == genreId)).ToListAsync();
        // }
     

        // public async Task<List<Anime>> SearchAsync(string keyword)
        // {
        //     return await _context.Animes.Where(a => a.Title.Contains(keyword) || a.Synopsis.Contains(keyword)).ToListAsync();
        // }
}
