using MediatR;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;


namespace OtakuTracker.Application.Animes.Create
{
    public record CreateAnime(string Title, string Synopsis, DateTime? StartDate, DateTime? EndDate,
        string Status, string Type, int Episodes, int Duration, string AgeRating, string PosterImageUrl,
        string TrailerUrl, decimal AverageRating, int TotalRatings, List<Genre> Genres, List<Theme> Themes)
        : IRequest<AnimeDto>;

    public class CreateAnimeHandler : IRequestHandler<CreateAnime, AnimeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAnimeHandler> _logger;
        private readonly IMapper _mapper;


        public CreateAnimeHandler(IUnitOfWork unitOfWork, ILogger<CreateAnimeHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _logger = logger;

        }

        public async Task<AnimeDto> Handle(CreateAnime request, CancellationToken cancellationToken)
        { 

            var anime = _mapper.Map<Anime>(request);


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var createdAnime = _unitOfWork.AnimeRepository.Create(anime);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Anime created successfully");

                return AnimeDto.FromAnime(createdAnime);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to create anime");

                throw;
            }
        }
    }
}
