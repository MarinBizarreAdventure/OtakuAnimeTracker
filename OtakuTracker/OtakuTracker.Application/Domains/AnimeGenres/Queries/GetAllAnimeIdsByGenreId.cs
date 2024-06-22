using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Domains.AnimeGenres.Queries;

public record GetAllAnimeIdsByGenreId(int GenreId) : IRequest<List<int>>;

public class GetAllAnimeIdsByGenreIdHandler : IRequestHandler<GetAllAnimeIdsByGenreId, List<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllAnimeIdsByGenreIdHandler> _logger;

    public GetAllAnimeIdsByGenreIdHandler(IUnitOfWork unitOfWork, ILogger<GetAllAnimeIdsByGenreIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<int>> Handle(GetAllAnimeIdsByGenreId request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting all anime IDs for genre with ID {request.GenreId}");

            var animeIds = await _unitOfWork.AnimeGenreRepository.GetAllAnimeIdsByGenreId(request.GenreId);

            _logger.LogInformation($"Retrieved {animeIds.Count} anime IDs for genre with ID {request.GenreId}");

            return animeIds;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get anime IDs for genre with ID {request.GenreId}");
            throw; // Re-throw the exception to propagate it to the caller
        }
    }
}
