using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class Anime
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnimeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Range(0, 10)]
    public double? Score { get; set; }

    [StringLength(100)]
    public string EnglishName { get; set; }

    [StringLength(100)]
    public string JapaneseName { get; set; }

    [Url]
    public string ImageUrl { get; set; }

    [StringLength(50)]
    public string Type { get; set; }

    [Range(0, int.MaxValue)]
    public int? Episodes { get; set; }

    public string Aired { get; set; }

    public string Premiered { get; set; }

    public string Producers { get; set; }

    public string Licensors { get; set; }

    public string Studios { get; set; }

    public string Source { get; set; }

    public string Duration { get; set; }

    public string Synopsis { get; set; }

    [StringLength(50)]
    public string Rating { get; set; }

    [Range(0, int.MaxValue)]
    public int? Ranked { get; set; }

    [Range(0, int.MaxValue)]
    public int? Popularity { get; set; }

    [Range(0, int.MaxValue)]
    public int? Members { get; set; }

    [Range(0, int.MaxValue)]
    public int? Favorites { get; set; }

    [Range(0, int.MaxValue)]
    public int? Watching { get; set; }

    [Range(0, int.MaxValue)]
    public int? Completed { get; set; }

    [Range(0, int.MaxValue)]
    public int? OnHold { get; set; }

    [Range(0, int.MaxValue)]
    public int? Dropped { get; set; }

    [Range(0, int.MaxValue)]
    public int? PlanToWatch { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score10 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score9 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score8 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score7 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score6 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score5 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score4 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score3 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score2 { get; set; }

    [Range(0, int.MaxValue)]
    public int? Score1 { get; set; }

    public virtual ICollection<AnimeGenre> AnimeGenres { get; set; } = new List<AnimeGenre>();

    public virtual ICollection<AnimeList> AnimeLists { get; set; } = new List<AnimeList>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}