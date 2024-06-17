namespace OtakuTracker.Domain.Models;

public class AnimeList
{
    public int UserId { get; set; }
    public List<AnimeListItem> AnimeItems { get; set; }

    public AnimeList(int userId)
    {
        AnimeItems = new List<AnimeListItem>();
    }
}