using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.AnimeLists.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.AnimeLists.Commands;


public record CreateAnimeList(
    [Required(ErrorMessage = "Username is required.")]
    string Username,
    
    [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
    int AnimeId,
    
    [Range(0, 10, ErrorMessage = "Score must be between 0 and 10.")]
    int? Score,
    
    [Range(0, int.MaxValue, ErrorMessage = "WatchingStatus must be a non-negative integer.")]
    int? WatchingStatus,
    
    [Range(0, int.MaxValue, ErrorMessage = "WatchedEpisodes must be a non-negative integer.")]
    int? WatchedEpisodes,
    
    [DataType(DataType.Date, ErrorMessage = "MyStartDate must be a valid date.")]
    DateOnly? MyStartDate,
    
    [DataType(DataType.Date, ErrorMessage = "MyFinishDate must be a valid date.")]
    DateOnly? MyFinishDate,
    
    [Range(0, int.MaxValue, ErrorMessage = "MyRewatching must be a non-negative integer.")]
    int? MyRewatching,
    
    [Range(0, int.MaxValue, ErrorMessage = "MyRewatchingEp must be a non-negative integer.")]
    int? MyRewatchingEp,
    
    [DataType(DataType.DateTime, ErrorMessage = "MyLastUpdated must be a valid date and time.")]
    DateTime? MyLastUpdated,
    
    [StringLength(100, ErrorMessage = "MyTags must be at most 100 characters long.")]
    string? MyTags
) : IRequest<AnimeListDto>;


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
            var errorMessage = "Failed to create AnimeList";
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}
    
    
