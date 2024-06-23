using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

public record RemoveAnimeFromUserList(
    [Required(ErrorMessage = "Username is required.")]
    string Username,
    
    [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
    int AnimeId
) : IRequest<bool>;

public class RemoveAnimeFromUserListHandler : IRequestHandler<RemoveAnimeFromUserList, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveAnimeFromUserList> _logger;

    public RemoveAnimeFromUserListHandler(IUnitOfWork unitOfWork, ILogger<RemoveAnimeFromUserList> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(RemoveAnimeFromUserList request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.AnimeListRepository.RemoveAnimeFromUserList(request.Username, request.AnimeId);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("AnimeList item deleted successfully");

            return true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            var errorMessage = "Failed to delete AnimeList item";
            _logger.LogError(ex, errorMessage);
            throw new Exception(errorMessage);
        }
    }
}