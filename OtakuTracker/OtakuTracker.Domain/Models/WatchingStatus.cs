using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuTracker.Domain.Models;

public class WatchingStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusId { get; set; }

    [Required]
    [StringLength(50)]
    public string? StatusDescription { get; set; }

    public virtual ICollection<AnimeList> AnimeLists { get; set; } = new List<AnimeList>();
}