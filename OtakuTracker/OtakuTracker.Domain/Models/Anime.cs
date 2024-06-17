namespace OtakuTracker.Domain.Models;

public class Anime
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Theme> Themes { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public int Episodes { get; set; }
    public int Duration { get; set; }
    public string AgeRating { get; set; }
    public string PosterImageUrl { get; set; }
    public string TrailerUrl { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRatings { get; set; }

}