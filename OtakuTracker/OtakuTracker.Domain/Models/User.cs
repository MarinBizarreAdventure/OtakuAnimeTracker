using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(100)] // Adjust as needed
    public string? Passwordhash { get; set; }

    [Range(0, int.MaxValue)]
    public int? UserWatching { get; set; }

    [Range(0, int.MaxValue)]
    public int? UserCompleted { get; set; }

    [Range(0, int.MaxValue)]
    public int? UserOnhold { get; set; }

    [Range(0, int.MaxValue)]
    public int? UserDropped { get; set; }

    [Range(0, int.MaxValue)]
    public int? UserPlantowatch { get; set; }

    [Range(0, double.MaxValue)]
    public double? UserDaysSpentWatching { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    public DateOnly? BirthDate { get; set; }

    [Range(0, int.MaxValue)]
    public int? AccessRank { get; set; }

    public DateTime? JoinDate { get; set; }

    public DateTime? LastOnline { get; set; }

    [Range(0, double.MaxValue)]
    public double? StatsMeanScore { get; set; }

    [Range(0, int.MaxValue)]
    public int? StatsRewatched { get; set; }

    [Range(0, int.MaxValue)]
    public int? StatsEpisodes { get; set; }

    public virtual ICollection<AnimeList> AnimeLists { get; set; } = new List<AnimeList>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}