using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class Anime
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnimeId { get; set; }

    public string? Name { get; set; }

    public double? Score { get; set; }

    public string? EnglishName { get; set; }

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

    public int? Members { get; set; }

    public int? Favorites { get; set; }

    public int? Watching { get; set; }

    public int? Completed { get; set; }

    public int? OnHold { get; set; }

    public int? Dropped { get; set; }

    public int? PlanToWatch { get; set; }

    public int? Score10 { get; set; }

    public int? Score9 { get; set; }

    public int? Score8 { get; set; }

    public int? Score7 { get; set; }

    public int? Score6 { get; set; }

    public int? Score5 { get; set; }

    public int? Score4 { get; set; }

    public int? Score3 { get; set; }

    public int? Score2 { get; set; }

    public int? Score1 { get; set; }

    public virtual ICollection<AnimeGenre> AnimeGenres { get; set; } = new List<AnimeGenre>();

    public virtual ICollection<AnimeList> AnimeLists { get; set; } = new List<AnimeList>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}