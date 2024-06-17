using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Queries
{
    public record GetAnimesByIds(List<int> AnimeIds) : IRequest<List<AnimeDto>>;

    public class GetAnimesByIdsHandler : IRequestHandler<GetAnimesByIds, List<AnimeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAnimesByIdsHandler> _logger;

        public GetAnimesByIdsHandler(IUnitOfWork unitOfWork, ILogger<GetAnimesByIdsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<AnimeDto>> Handle(GetAnimesByIds request, CancellationToken cancellationToken)
        {

            try
            {
                var animeList = _unitOfWork.AnimeRepository.GetAnimesByIds(request.AnimeIds);
                var animeDtoList = new List<AnimeDto>();

                foreach (var anime in animeList)
                {
                    animeDtoList.Add(AnimeDto.FromAnime(anime));
                }

                _logger.LogInformation("Animes retrieved successfully");
                return animeDtoList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get animes by IDs");
                throw;
            }
        }
    }
}
