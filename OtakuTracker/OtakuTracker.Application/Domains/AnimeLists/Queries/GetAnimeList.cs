using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.AnimeLists.Responses;

namespace OtakuTracker.Application.AnimeLists.Queries;

public record GetAnimeList(
    [Required(ErrorMessage = "Username is required.")]
    string Username
) : IRequest<List<AnimeListDto>>;
public class GetAnimeListHandler : IRequestHandler<GetAnimeList, List<AnimeListDto>>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAnimeListHandler> _logger;
    private readonly IMapper _mapper;

    public GetAnimeListHandler(IUnitOfWork unitOfWork, ILogger<GetAnimeListHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<AnimeListDto>> Handle(GetAnimeList request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Handling request to get anime list for user: {request.Username}");

            var animeList = await _unitOfWork.AnimeListRepository.GetUserListByUserId(request.Username);

            if (animeList == null)
            {
                _logger.LogWarning($"Anime list for user: {request.Username} not found");
                return new List<AnimeListDto>();
            }

            var animeListDto = _mapper.Map<List<AnimeListDto>>(animeList);

            _logger.LogInformation($"Anime list for user: {request.Username} found");
            return animeListDto;
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error occurred while getting anime list for user: {request.Username}";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}