namespace OtakuTracker.Domain.Models;

public class WatchingStatus
{
    public int StatusId { get; set; }

    public string? StatusDescription { get; set; }

    public virtual ICollection<AnimeList> AnimeLists { get; set; } = new List<AnimeList>();
}