using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Genres.Responses;

namespace OtakuTracker.Application.Genres.Queries;

public record GetGenreById(int GenreId) : IRequest<GenreDto>;

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
        _logger.LogInformation("Handling request to get genre by ID");

        var genre = await _unitOfWork.GenresRepository.GetGenresById(request.GenreId);

        if (genre == null)
        {
            _logger.LogWarning($"Genre with ID {request.GenreId} not found");
            return null; // Or throw an exception if required
        }

        var genreDto = _mapper.Map<GenreDto>(genre);

        _logger.LogInformation($"Genre with ID {request.GenreId} found");
        return genreDto;
    }
}