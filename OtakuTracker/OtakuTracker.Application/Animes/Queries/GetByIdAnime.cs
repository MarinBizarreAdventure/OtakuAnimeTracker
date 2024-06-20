using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Queries
{
    public record GetByIdAnime(int AnimeId) : IRequest<AnimeDto>;
    public class GetByIdAnimeHandler : IRequestHandler<GetByIdAnime, AnimeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetByIdAnimeHandler> _logger;
        private readonly IMapper _mapper;

        public GetByIdAnimeHandler(IUnitOfWork unitOfWork, ILogger<GetByIdAnimeHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AnimeDto> Handle(GetByIdAnime request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to get anime by ID");

            var animeDto = _mapper.Map<AnimeDto>(_unitOfWork.AnimeRepository.GetById(request.AnimeId));
            if (animeDto == null)
            {
                _logger.LogWarning($"Anime with ID {request.AnimeId} not found");
                return null; // Or throw an exception if required
            }

            _logger.LogInformation($"Anime with ID {request.AnimeId} found");
            return await Task.FromResult(animeDto);
        }
    }
}
