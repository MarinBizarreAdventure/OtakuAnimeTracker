using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Animes.Commands
{
    public record DeleteAnime(int AnimeId) : IRequest<bool>;

    public class DeleteAnimeHandler : IRequestHandler<DeleteAnime, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAnimeHandler> _logger;

        public DeleteAnimeHandler(IUnitOfWork unitOfWork, ILogger<DeleteAnimeHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteAnime request, CancellationToken cancellationToken)
        {

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                _unitOfWork.AnimeRepository.Delete(request.AnimeId);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Anime deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to delete anime with ID: {AnimeId}", request.AnimeId);
                throw;
            }
        }
    }
}
