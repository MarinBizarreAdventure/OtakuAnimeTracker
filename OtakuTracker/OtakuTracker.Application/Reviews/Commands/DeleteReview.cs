using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;

namespace OtakuTracker.Application.Reviews.Commands
{
    public record DeleteReview(int ReviewId) : IRequest<bool>;

    public class DeleteReviewHandler : IRequestHandler<DeleteReview, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteReviewHandler> _logger;

        public DeleteReviewHandler(IUnitOfWork unitOfWork, ILogger<DeleteReviewHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteReview request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to delete review");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var review = _unitOfWork.ReviewRepository.GetReviewById(request.ReviewId);
                if (review == null)
                {
                    _logger.LogWarning("Review not found");
                    return false;
                }

                _unitOfWork.ReviewRepository.DeleteReview(review.ReviewId);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Review deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to delete review");
                throw;
            }
        }
    }
}
