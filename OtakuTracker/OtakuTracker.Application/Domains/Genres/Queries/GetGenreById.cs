using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Genres.Responses;

namespace OtakuTracker.Application.Genres.Queries;

public record GetGenreById(
    [Range(1, int.MaxValue, ErrorMessage = "GenreId must be a positive integer.")]
    int GenreId
) : IRequest<GenreDto>;
public class GetGenreByIdHandler : IRequestHandler<GetGenreById, GenreDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetGenreByIdHandler> _logger;
    private readonly IMapper _mapper;

    public GetGenreByIdHandler(IUnitOfWork unitOfWork, ILogger<GetGenreByIdHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenreDto> Handle(GetGenreById request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling request to get genre by ID");

            var genre = await _unitOfWork.GenresRepository.GetGenresById(request.GenreId);

            if (genre == null)
            {
                _logger.LogWarning($"Genre with ID {request.GenreId} not found");
                return null;
            }

            var genreDto = _mapper.Map<GenreDto>(genre);

            _logger.LogInformation($"Genre with ID {request.GenreId} found");
            return genreDto;
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error occurred while getting genre with ID {request.GenreId}";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}