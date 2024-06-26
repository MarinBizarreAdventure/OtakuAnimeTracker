using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Domains.AnimeGenres.Responses;
using OtakuTracker.Domain.Models;

public record CreateAnimeGenres(int AnimeId, List<int> GenreIds) : IRequest<List<AnimeGenreDto>>;

public class CreateAnimeGenresHandler : IRequestHandler<CreateAnimeGenres, List<AnimeGenreDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAnimeGenresHandler> _logger;

    public CreateAnimeGenresHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnimeGenresHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<AnimeGenreDto>> Handle(CreateAnimeGenres request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling request to create anime genres.");

            var animeGenres = new List<AnimeGenre>();
            foreach (var genreId in request.GenreIds)
            {
                animeGenres.Add(new AnimeGenre { AnimeId = request.AnimeId, GenreId = genreId });
            }

            await _unitOfWork.AnimeGenreRepository.AddRange(animeGenres);

            _logger.LogInformation($"Created {animeGenres.Count} anime genres.");

            return animeGenres.Select(ag => _mapper.Map<AnimeGenreDto>(ag)).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create anime genres.");
            throw; 
        }
    }
}

