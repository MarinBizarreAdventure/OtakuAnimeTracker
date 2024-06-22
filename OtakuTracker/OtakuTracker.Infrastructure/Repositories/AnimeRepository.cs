using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
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
            var existingAnime = await _context.Animes.FindAsync(anime.AnimeId);
            if (existingAnime != null)
            {
                _context.Entry(existingAnime).State = EntityState.Detached; // Detach the existing entity
                _context.Animes.Update(anime); // Attach the new entity
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Anime with ID {anime.AnimeId} not found");
            }
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

        public async Task<List<AnimeSummaryDto>> GetTopAnimes(int page, int pageSize)
        {
            return await _context.Animes
                .OrderByDescending(a => a.Rating)
                .Select(a => new AnimeSummaryDto
                {
                    AnimeId = a.AnimeId.ToString(),
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Rating = a.Rating
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<AnimeSummaryDto>> GetUpcomingAnimes(int page, int pageSize)
        {
            // Implement logic to get upcoming animes
            // Example:
            return await Task.FromResult(new List<AnimeSummaryDto>());
        }

        public async Task<List<AnimeSummaryDto>> GetAiringAnimes(int page, int pageSize)
        {
            // Implement logic to get airing animes
            // Example:
            return await Task.FromResult(new List<AnimeSummaryDto>());
        }
}
