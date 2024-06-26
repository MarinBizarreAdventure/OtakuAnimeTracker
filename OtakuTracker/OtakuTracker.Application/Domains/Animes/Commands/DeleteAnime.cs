﻿using MediatR;
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
                return isDeleted; // Return the result of deletion operation
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
