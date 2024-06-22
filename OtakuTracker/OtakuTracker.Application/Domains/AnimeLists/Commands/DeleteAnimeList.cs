using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

public record RemoveAnimeFromUserList(string Username, int AnimeId) : IRequest<bool>;

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
            await _unitOfWork.AnimeListRepository.RemoveAnimeFromUserList(request.Username, request.AnimeId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("AnimeList item deleted successfully");

            return true;
        }
        catch (Exception ex) 
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Failed to delete AnimeList item");
            throw;
        }
    }
}