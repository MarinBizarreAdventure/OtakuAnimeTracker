using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Animes.Queries;

public record CountAnimesQuery : IRequest<int>;

public class CountAnimesQueryHandler : IRequestHandler<CountAnimesQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CountAnimesQueryHandler> _logger;

    public CountAnimesQueryHandler(IUnitOfWork unitOfWork, ILogger<CountAnimesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(CountAnimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling request to count animes");

            var count = await _unitOfWork.AnimeRepository.CountAnimes();

            _logger.LogInformation($"Total number of animes: {count}");
            return count;
        }
        catch (Exception ex)
        {
            var errorMessage = "Error occurred while counting animes";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}