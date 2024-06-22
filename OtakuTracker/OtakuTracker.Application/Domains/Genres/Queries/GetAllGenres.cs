using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Genres.Responses;

namespace OtakuTracker.Application.Genres.Queries;

public record GetAllGenres : IRequest<List<GenreDto>>;

public class GetAllGenresHandler : IRequestHandler<GetAllGenres, List<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllGenresHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllGenresHandler(IUnitOfWork unitOfWork, ILogger<GetAllGenresHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling request to get all genres");

        var genres = await _unitOfWork.GenresRepository.GetAllGenres();
        var genreDtos = _mapper.Map<List<GenreDto>>(genres);

        _logger.LogInformation($"Found {genreDtos.Count} genres");
        return genreDtos;
    }
}
