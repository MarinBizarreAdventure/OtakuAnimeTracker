namespace OtakuTracker.Domain.Models;

public class RatingComplete
{
    public int UserId { get; set; }

    public int AnimeId { get; set; }
    
    public int? Rating { get; set; }

    public virtual Anime Anime { get; set; } = null!;
}