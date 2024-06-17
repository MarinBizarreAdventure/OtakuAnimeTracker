namespace OtakuTracker.Domain.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int AnimeId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime ReviewDate { get; set; }
}