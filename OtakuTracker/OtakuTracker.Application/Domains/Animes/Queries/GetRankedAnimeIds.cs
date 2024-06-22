using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Animes.Queries;

public record GetRankedAnimeIds(int Page, int PageSize, string SortOrder) : IRequest<List<int>>;

public class GetRankedAnimeIdsHandler : IRequestHandler<GetRankedAnimeIds, List<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRankedAnimeIdsHandler> _logger;

    public GetRankedAnimeIdsHandler(IUnitOfWork unitOfWork, ILogger<GetRankedAnimeIdsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<int>> Handle(GetRankedAnimeIds request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling request to get ranked anime IDs");

            var animeIds = await _unitOfWork.AnimeRepository.GetRankedAnimeIds(request.Page, request.PageSize, request.SortOrder);
            return animeIds;
        }
        catch (Exception ex)
        {
            var errorMessage = "Failed to get ranked anime IDs";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}
