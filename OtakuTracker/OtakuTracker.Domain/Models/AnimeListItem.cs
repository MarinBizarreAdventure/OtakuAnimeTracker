using OtakuTracker.Domain.Enumerations;

namespace OtakuTracker.Domain.Models;

public class AnimeListItem
{
    public int UserId { get; set; }
    public int AnimeId { get; set; }
    public AnimeStatus Status { get; set; }
    public decimal Score { get; set; }
    public DateTime LastUpdated { get; set; }
}