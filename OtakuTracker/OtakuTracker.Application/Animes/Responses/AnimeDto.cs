using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Animes.Responses
{
    public class AnimeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Episodes { get; set; }
        public int Duration { get; set; }
        public string AgeRating { get; set; }
        public string PosterImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Theme> Themes { get; set; }

        public static AnimeDto FromAnime(Anime anime)
        {
            return new AnimeDto
            {
                Id = anime.Id,
                Title = anime.Title,
                Synopsis = anime.Synopsis,
                StartDate = anime.StartDate,
                EndDate = anime.EndDate,
                Status = anime.Status,
                Type = anime.Type,
                Episodes = anime.Episodes,
                Duration = anime.Duration,
                AgeRating = anime.AgeRating,
                PosterImageUrl = anime.PosterImageUrl,
                TrailerUrl = anime.TrailerUrl,
                AverageRating = anime.AverageRating,
                TotalRatings = anime.TotalRatings,
                Genres = anime.Genres,
                Themes = anime.Themes
            };
        }
      }
    }
