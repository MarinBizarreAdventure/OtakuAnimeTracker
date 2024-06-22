using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Domains.AnimeGenres.Queries;

public record GetAllGenreIdsByAnimeId(int AnimeId) : IRequest<List<int>>;

public class GetAllGenreIdsByAnimeIdHandler : IRequestHandler<GetAllGenreIdsByAnimeId, List<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllGenreIdsByAnimeIdHandler> _logger;

    public GetAllGenreIdsByAnimeIdHandler(IUnitOfWork unitOfWork, ILogger<GetAllGenreIdsByAnimeIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        
    }

    public async Task<List<int>> Handle(GetAllGenreIdsByAnimeId request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting all genre IDs for anime with ID {request.AnimeId}");

            var genreIds = await _unitOfWork.AnimeGenreRepository.GetAllGenreIdsByAnimeId(request.AnimeId);

            _logger.LogInformation($"Retrieved {genreIds.Count} genre IDs for anime with ID {request.AnimeId}");

            return genreIds;
        }
        catch (Exception ex)
        {
            var errorMessage = $"Failed to get genre IDs for anime with ID {request.AnimeId}";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}