using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

public record GetPopularAnimeIds(
    [Range(1, int.MaxValue, ErrorMessage = "Page must be a positive integer.")]
    int Page,
    
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    int PageSize,
    
    [RegularExpression("^(asc|desc)$", ErrorMessage = "SortOrder must be 'asc' or 'desc'.")]
    string SortOrder
) : IRequest<List<int>>;


public class GetPopularAnimeIdsHandler : IRequestHandler<GetPopularAnimeIds, List<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPopularAnimeIdsHandler> _logger;

    public GetPopularAnimeIdsHandler(IUnitOfWork unitOfWork, ILogger<GetPopularAnimeIdsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<int>> Handle(GetPopularAnimeIds request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling request to get popular anime IDs");

            var animeIds = await _unitOfWork.AnimeRepository.GetPopularAnimeIds(request.Page, request.PageSize, request.SortOrder);
            return animeIds;
        }
        catch (Exception ex)
        {
            var errorMessage = "Failed to get popular anime IDs";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}