using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Queries;

public record GetAnimeSummaryById(
    [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
    int AnimeId
) : IRequest<AnimeSummaryDto>;

public class GetAnimeSummaryByIdHandler : IRequestHandler<GetAnimeSummaryById, AnimeSummaryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAnimeSummaryByIdHandler> _logger;
    private readonly IMapper _mapper;

    public GetAnimeSummaryByIdHandler(IUnitOfWork unitOfWork, ILogger<GetAnimeSummaryByIdHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AnimeSummaryDto> Handle(GetAnimeSummaryById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get anime summary by ID");

        try
        {
            var anime = await _unitOfWork.AnimeRepository.GetAnimeSummaryById(request.AnimeId);
            
            if (anime == null)
            {
                _logger.LogWarning($"Anime with ID {request.AnimeId} not found");
                return null; 
            }

            _logger.LogInformation($"Anime with ID {request.AnimeId} found");
            return anime;
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error occurred while getting anime summary for ID {request.AnimeId}";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}
