namespace OtakuTracker.Domain.Models;

public class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? AnimeId { get; set; }

    public string? ReviewText { get; set; }

    public int? Rating { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public virtual Anime? Anime { get; set; }

    public virtual User? User { get; set; }
}