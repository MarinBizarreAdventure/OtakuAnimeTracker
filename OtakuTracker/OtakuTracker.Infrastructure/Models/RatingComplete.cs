using System;
using System.Collections.Generic;

namespace OtakuTracker.Infrastructure.Models;

public partial class RatingComplete
{
    public int UserId { get; set; }

    public int AnimeId { get; set; }

    public int? Rating { get; set; }

    public virtual Anime Anime { get; set; } = null!;
}
