using OtakuTracker.Domain.Models;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Infrastructure.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private readonly AnimeDbContext _context;

    public AnimeRepository(AnimeDbContext context)
    {
        _context = context;
    }

    public Anime Create(Anime anime)
    {
        _context.Animes.Add(anime);
        _context.SaveChanges();
        return anime;
    }

    public Anime GetById(int animeId)
    {
        return _context.Animes.Find(animeId);
    }

    public List<Anime> GetAnimesByIds(List<int> animeIds)
    {
        return _context.Animes.Where(a => animeIds.Contains(a.Id)).ToList();
    }

    public void Update(Anime anime)
    {
        _context.Animes.Update(anime);
        _context.SaveChanges();
    }

    public void Delete(int animeId)
    {
        var anime = _context.Animes.Find(animeId);
        if (anime != null)
        {
            _context.Animes.Remove(anime);
            _context.SaveChanges();
        }
    }

    public List<Anime> GetAll()
    {
        return _context.Animes.ToList();
    }

    public List<Anime> GetByGenre(int genreId)
    {
        return _context.Animes.Where(a => a.Genres.Any(g => g.GenreId == genreId)).ToList();
    }

    public List<Anime> GetByTheme(int themeId)
    {
        return _context.Animes.Where(a => a.Themes.Any(t => t.ThemeId == themeId)).ToList();
    }

    public List<Anime> GetByStatus(string status)
    {
        return _context.Animes.Where(a => a.Status == status).ToList();
    }

    public List<Anime> Search(string keyword)
    {
        return _context.Animes.Where(a => a.Title.Contains(keyword) || a.Synopsis.Contains(keyword)).ToList();
    }
}