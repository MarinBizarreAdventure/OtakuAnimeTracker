using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Animes.Commands
{
    public record DeleteAnime(
        [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
        int AnimeId
    ) : IRequest<bool>;
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
                var isDeleted = await _unitOfWork.AnimeRepository.Delete(request.AnimeId);
                if (isDeleted)
                {
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("Anime deleted successfully");
                }
                else
                {
                    _logger.LogInformation("Anime not found");
                }
                return isDeleted; 
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                var errorMessage = $"Failed to delete anime with ID: {request.AnimeId}";

                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
