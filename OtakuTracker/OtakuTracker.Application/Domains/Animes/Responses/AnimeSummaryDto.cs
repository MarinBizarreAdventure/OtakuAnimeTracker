namespace OtakuTracker.Application.Animes.Responses;

public class AnimeSummaryDto
{
    public int? AnimeId { get; set; }
    public string? Name { get; set; }
    public string? JapaneseName { get; set; }
    public string? ImageUrl { get; set; }
    public string? Type { get; set; }
    public int? Episodes { get; set; }
    public string? Aired { get; set; }
    public string? Premiered { get; set; }
    public string? Producers { get; set; }
    public string? Licensors { get; set; }
    public string? Studios { get; set; }
    public string? Source { get; set; }
    public string? Duration { get; set; }
    public string? Synopsis { get; set; }
    public string? Rating { get; set; }
    public int? Ranked { get; set; }
    public int? Popularity { get; set; }
    
}