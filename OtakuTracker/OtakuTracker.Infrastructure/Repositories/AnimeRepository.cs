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

        
        public async Task<List<int>> GetPopularAnimeIds(int page, int pageSize, string sortOrder)
        {
            var query = _context.Animes.AsQueryable();
            if (sortOrder.ToLower() == "asc")
            {
                query = query.OrderBy(a => a.Popularity);
            }
            else
            {
                query = query.OrderByDescending(a => a.Popularity);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => a.AnimeId)
                .ToListAsync();
        }

        public async Task<List<int>> GetRankedAnimeIds(int page, int pageSize, string sortOrder)
        {
            var query = _context.Animes.AsQueryable();
            if (sortOrder?.ToLower() == "asc")
            {
                query = query.OrderBy(a => a.Ranked);
            }
            else if (sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(a => a.Ranked);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => a.AnimeId)
                .ToListAsync();
        }

        public async Task<AnimeSummaryDto> GetAnimeSummaryById(int animeId)
        {
            var anime = await _context.Animes
                .Where(a => a.AnimeId == animeId)
                .Select(a => new AnimeSummaryDto
                {
                    AnimeId = a.AnimeId,
                    Name = a.Name,
                    JapaneseName = a.JapaneseName,
                    ImageUrl = a.ImageUrl,
                    Type = a.Type,
                    Episodes = a.Episodes,
                    Aired = a.Aired,
                    Premiered = a.Premiered,
                    Producers = a.Producers,
                    Licensors = a.Licensors,
                    Studios = a.Studios,
                    Source = a.Source,
                    Duration = a.Duration,
                    Synopsis = a.Synopsis,
                    Rating = a.Rating,
                    Ranked = a.Ranked,
                    Popularity = a.Popularity
                })
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                throw new Exception($"Anime with ID {animeId} not found");
            }

            return anime;
        }
        
        public async Task<int> CountAnimes()
        {
            return await _context.Animes.CountAsync();
        }

        
}
