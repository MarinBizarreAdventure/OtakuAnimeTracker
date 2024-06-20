using System;
using System.Collections.Generic;

namespace OtakuTracker.Infrastructure.Models;

public partial class AnimeList
{
    public string Username { get; set; } = null!;

    public int AnimeId { get; set; }

    public int? Score { get; set; }

    public int? WatchingStatus { get; set; }

    public int? WatchedEpisodes { get; set; }

    public DateOnly? MyStartDate { get; set; }

    public DateOnly? MyFinishDate { get; set; }

    public int? MyRewatching { get; set; }

    public int? MyRewatchingEp { get; set; }

    public DateTime? MyLastUpdated { get; set; }

    public string? MyTags { get; set; }

    public virtual Anime Anime { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual WatchingStatus? WatchingStatusNavigation { get; set; }
}
