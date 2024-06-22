using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Infrastructure.Repositories;

public class GenreRepository : IGenresRepository
{
    private readonly OtakutrackerContext _context;

    public GenreRepository(OtakutrackerContext context)
    {
        _context = context;
    }

    public async Task<Genre> GetGenresById(int genreId)
    {
        return await _context.Genres.FindAsync(genreId);
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        return await _context.Genres.ToListAsync();
    }
}