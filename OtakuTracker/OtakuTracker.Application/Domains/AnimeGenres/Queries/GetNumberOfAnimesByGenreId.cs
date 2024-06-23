using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;



public record GetNumberOfAnimesByGenreIdQuery(int GenreId) : IRequest<int>;

public class GetNumberOfAnimesByGenreIdQueryHandler : IRequestHandler<GetNumberOfAnimesByGenreIdQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetNumberOfAnimesByGenreIdQueryHandler> _logger;

    public GetNumberOfAnimesByGenreIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetNumberOfAnimesByGenreIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(GetNumberOfAnimesByGenreIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get number of animes by genre ID");

        var count = await _unitOfWork.AnimeGenreRepository.GetNumberOfAnimesByGenreId(request.GenreId);

        if (count == 0)
        {
            var errorMessage = $"No animes found for genre ID {request.GenreId}";
            _logger.LogWarning(errorMessage);
            throw new Exception(errorMessage);
        }

        _logger.LogInformation($"Found {count} animes for genre ID {request.GenreId}");
        return count;
    }
}
