using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; }

    [ForeignKey("Anime")]
    public int? AnimeId { get; set; }

    [StringLength(1000)]
    public string? ReviewText { get; set; }

    [Range(0, 10)]
    public int? Rating { get; set; }
    public DateOnly? ReviewDate { get; set; }

    public virtual Anime? Anime { get; set; }

    public virtual User? User { get; set; }
}