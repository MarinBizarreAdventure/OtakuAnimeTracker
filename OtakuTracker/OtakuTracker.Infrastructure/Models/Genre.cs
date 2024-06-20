using System;
using System.Collections.Generic;

namespace OtakuTracker.Infrastructure.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Anime> Animes { get; set; } = new List<Anime>();
}
