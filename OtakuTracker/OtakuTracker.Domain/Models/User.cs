namespace OtakuTracker.Domain.Models;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? Passwordhash { get; set; }

    public int? UserWatching { get; set; }

    public int? UserCompleted { get; set; }

    public int? UserOnhold { get; set; }

    public int? UserDropped { get; set; }

    public int? UserPlantowatch { get; set; }

    public double? UserDaysSpentWatching { get; set; }

    public string? Gender { get; set; }

    public string? Location { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int? AccessRank { get; set; }

    public DateOnly? JoinDate { get; set; }

    public DateTime? LastOnline { get; set; }

    public double? StatsMeanScore { get; set; }

    public int? StatsRewatched { get; set; }

    public int? StatsEpisodes { get; set; }

    public virtual ICollection<AnimeList>? AnimeLists { get; set; } = new List<AnimeList>();

    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
}
