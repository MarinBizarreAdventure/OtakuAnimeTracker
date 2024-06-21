using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.AnimeLists.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.AnimeLists.Commands;


public record CreateAnimeList(string Username, int AnimeId, int? Score, int? WatchingStatus, int? WatchedEpisodes,
    DateOnly? MyStartDate, DateOnly? MyFinishDate, int? MyRewatching, int? MyRewatchingEp, DateTime? MyLastUpdated, 
    string? MyTags) : IRequest<AnimeListDto>;

public class CreateAnimeListHandler : IRequestHandler<CreateAnimeList, AnimeListDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAnimeListHandler> _logger;
    private readonly IMapper _mapper;

    public CreateAnimeListHandler(IUnitOfWork unitOfWork, ILogger<CreateAnimeListHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnimeListDto> Handle(CreateAnimeList request, CancellationToken cancellationToken)
    {
        var animeList = _mapper.Map<AnimeList>(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var createdAnimeList = await _unitOfWork.AnimeListRepository.CreateUserList(animeList);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("AnimeList created successfully");

            return AnimeListDto.FromAnimeList(createdAnimeList);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Failed to create AnimeList");
            throw;
        }
    }
}
    
    
