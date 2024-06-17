using MediatR;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;
using Microsoft.Extensions.Logging;


namespace OtakuTracker.Application.Animes.Commands
{
    public record UpdateAnime(int AnimeId, string Title, string Synopsis, DateTime? StartDate, DateTime? EndDate,
    string Status, string Type, int Episodes, int Duration, string AgeRating, string PosterImageUrl,
    string TrailerUrl, decimal AverageRating, int TotalRatings, List<Genre> Genres, List<Theme> Themes)
    : IRequest<AnimeDto>;


    public class UpdateAnimeHandler : IRequestHandler<UpdateAnime, AnimeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAnimeHandler> _logger;

        public UpdateAnimeHandler(IUnitOfWork unitOfWork, ILogger<UpdateAnimeHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AnimeDto> Handle(UpdateAnime request, CancellationToken cancellationToken)
        {

            var updatedAnime = new Anime
            {
                Id = request.AnimeId,
                Title = request.Title,
                Synopsis = request.Synopsis,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                Type = request.Type,
                Episodes = request.Episodes,
                Duration = request.Duration,
                AgeRating = request.AgeRating,
                PosterImageUrl = request.PosterImageUrl,
                TrailerUrl = request.TrailerUrl,
                AverageRating = request.AverageRating,
                TotalRatings = request.TotalRatings,
                Genres = request.Genres,
                Themes = request.Themes
            };

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                _unitOfWork.AnimeRepository.Update(updatedAnime);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Anime updated successfully");
                return AnimeDto.FromAnime(updatedAnime);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to update anime");
                throw;
            }
        }
    }
}
