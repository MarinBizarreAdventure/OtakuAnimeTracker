using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class AnimeGenre
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Anime")]
    public int AnimeId { get; set; }
    
    [ForeignKey("Genre")]
    public int GenreId { get; set; }

    public virtual Anime Anime { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
