namespace OtakuTracker.Domain.Models;

public class Recommendation
{
    public int RecommendationId { get; set; }
    public int UserId { get; set; }
    public int AnimeId { get; set; }
    public string RecommendationText { get; set; }
    public DateTime RecommendationDate { get; set; }
}