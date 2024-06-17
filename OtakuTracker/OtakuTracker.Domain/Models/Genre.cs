namespace OtakuTracker.Domain.Models;

public class Genre
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public List<Anime> Animes { get; set; }
     
}