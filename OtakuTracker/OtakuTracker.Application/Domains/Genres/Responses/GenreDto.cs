using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Genres.Responses;

public class GenreDto
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }

    public static GenreDto FromGenre(Genre genre)
    {
        return new GenreDto
        {
            GenreId = genre.GenreId,
            GenreName = genre.GenreName
        };
    }
}