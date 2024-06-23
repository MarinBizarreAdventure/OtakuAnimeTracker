using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Infrastructure.Repositories;

public class AnimeGenreRepository : IAnimeGenreRepository
{
    private readonly OtakutrackerContext _context;

    public AnimeGenreRepository(OtakutrackerContext context)
    {
        _context = context;
    }

    public async Task<AnimeGenre> Add(AnimeGenre animeGenre)
    {
        var existingAnimeGenre = await _context.AnimeGenres
            .FirstOrDefaultAsync(ag => ag.AnimeId == animeGenre.AnimeId && ag.GenreId == animeGenre.GenreId);
        
        if (existingAnimeGenre == null)
        {
            _context.AnimeGenres.Add(animeGenre);
        }
        else
        {
            _context.Entry(existingAnimeGenre).CurrentValues.SetValues(animeGenre);
        }

        await _context.SaveChangesAsync();

        return animeGenre;

    }

    public async Task<AnimeGenre> GetById(int animeGenreId)
    {
        return await _context.AnimeGenres.FindAsync(animeGenreId);
    }

    public async Task Update(AnimeGenre animeGenre)
    {
        _context.AnimeGenres.Update(animeGenre);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int animeGenreId)
    {
        var animeGenre = await GetById(animeGenreId);
        if (animeGenre == null)
            return false;

        _context.AnimeGenres.Remove(animeGenre);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<int>> GetAllGenreIdsByAnimeId(int animeId)
    {
        return await _context.AnimeGenres
            .Where(ag => ag.AnimeId == animeId)
            .Select(ag => ag.GenreId)
            .ToListAsync();
    }

    public async Task<List<int>> GetAllAnimeIdsByGenreId(int genreId, int page, int pageSize)
    {
        return await _context.AnimeGenres
            .Where(ag => ag.GenreId == genreId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(ag => ag.AnimeId)
            .ToListAsync();
    }
    
    public async Task AddRange(List<AnimeGenre> animeGenres)
    {
        // _context.AnimeGenres.AddRange(animeGenres);
        // await _context.SaveChangesAsync();
        var animeIds = animeGenres.Select(ag => ag.AnimeId).Distinct().ToList();
        var genreIds = animeGenres.Select(ag => ag.GenreId).Distinct().ToList();

        var existingAnimeGenres = await _context.AnimeGenres
            .Where(ag => animeIds.Contains(ag.AnimeId) && genreIds.Contains(ag.GenreId))
            .ToListAsync();

        var newAnimeGenres = animeGenres
            .Where(newAg => !existingAnimeGenres
                .Any(existingAg => existingAg.AnimeId == newAg.AnimeId && existingAg.GenreId == newAg.GenreId))
            .ToList();

        if (newAnimeGenres.Any())
        {
            _context.AnimeGenres.AddRange(newAnimeGenres);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<int> GetNumberOfAnimesByGenreId(int genreId)
    {
        return await _context.AnimeGenres
            .Where(ag => ag.GenreId == genreId)
            .CountAsync();
    }
    
    
}