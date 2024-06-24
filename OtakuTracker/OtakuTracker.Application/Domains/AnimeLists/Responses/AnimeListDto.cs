using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.AnimeLists.Responses;

public class AnimeListDto
{
        public string Username { get; set; } = null!;
        public int AnimeId { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int? Score { get; set; }
        public int? WatchingStatus { get; set; }
        public int? WatchedEpisodes { get; set; }
        public DateOnly? MyStartDate { get; set; }
        public DateOnly? MyFinishDate { get; set; }
        public int? MyRewatching { get; set; }
        public int? MyRewatchingEp { get; set; }
        public DateTime? MyLastUpdated { get; set; }
        public string? MyTags { get; set; }

        public static AnimeListDto FromAnimeList(AnimeList animeList)
        {
                return new AnimeListDto
                {
                        Username = animeList.Username,
                        AnimeId = animeList.AnimeId,
                      
                        Score = animeList.Score,
                        WatchingStatus = animeList.WatchingStatus,
                        WatchedEpisodes = animeList.WatchedEpisodes,
                        MyStartDate = animeList.MyStartDate,
                        MyFinishDate = animeList.MyFinishDate,
                        MyRewatching = animeList.MyRewatching,
                        MyRewatchingEp = animeList.MyRewatchingEp,
                        MyLastUpdated = animeList.MyLastUpdated,
                        MyTags = animeList.MyTags
                        
                };
        }
}