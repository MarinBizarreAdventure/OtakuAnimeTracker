using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Queries;

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
            try
            {
                _logger.LogInformation("Handling request to get anime by ID");

                var anime = await _unitOfWork.AnimeRepository.GetById(request.AnimeId);

                if (anime == null)
                {
                    var errorMessage = $"Anime with ID {request.AnimeId} not found";
                    _logger.LogWarning(errorMessage);
                    throw new Exception(errorMessage); 
                }

                var animeDto = _mapper.Map<AnimeDto>(anime);

                _logger.LogInformation($"Anime with ID {request.AnimeId} found");
                return animeDto;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occurred while getting anime by ID {request.AnimeId}";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
        
    }
