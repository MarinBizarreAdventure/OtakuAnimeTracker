using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Animes.Responses;

    public class AnimeDto
    {
        public int? AnimeId { get; set; }
        public string? Name { get; set; }
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
        public List<GenreDto>? Genres { get; set; }

        public static AnimeDto FromAnime(Anime anime)
        {
            var genreDtos = anime.Genres.Select(g => new GenreDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName
            }).ToList();
            return new AnimeDto
            {
                AnimeId = anime.AnimeId,
                Name = anime.Name,
                EnglishName = anime.EnglishName,
                JapaneseName = anime.JapaneseName,
                ImageUrl = anime.ImageUrl,
                Type = anime.Type,
                Episodes = anime.Episodes,
                Aired = anime.Aired,
                Premiered = anime.Premiered,
                Producers = anime.Producers,
                Licensors = anime.Licensors,
                Studios = anime.Studios,
                Source = anime.Source,
                Duration = anime.Duration,
                Synopsis = anime.Synopsis,
                Rating = anime.Rating,
                Ranked = anime.Ranked,
                Popularity = anime.Popularity,
                Members = anime.Members,
                Favorites = anime.Favorites,
                Watching = anime.Watching,
                Completed = anime.Completed,
                OnHold = anime.OnHold,
                Dropped = anime.Dropped,
                PlanToWatch = anime.PlanToWatch,
                Score10 = anime.Score10,
                Score9 = anime.Score9,
                Score8 = anime.Score8,
                Score7 = anime.Score7,
                Score6 = anime.Score6,
                Score5 = anime.Score5,
                Score4 = anime.Score4,
                Score3 = anime.Score3,
                Score2 = anime.Score2,
                Score1 = anime.Score1,
                Genres = genreDtos
            };
        }
    }
