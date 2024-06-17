using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;

namespace OtakuTracker.Application.Reviews.Queries
{
    public record GetReviewById(int ReviewId) : IRequest<ReviewDto>;

    public class GetReviewByIdHandler : IRequestHandler<GetReviewById, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetReviewByIdHandler> _logger;

        public GetReviewByIdHandler(IUnitOfWork unitOfWork, ILogger<GetReviewByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ReviewDto> Handle(GetReviewById request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to get review by ID: {ReviewId}", request.ReviewId);

            var review = _unitOfWork.ReviewRepository.GetReviewById(request.ReviewId);

            if (review != null)
            {
                _logger.LogInformation("Review found with ID: {ReviewId}", request.ReviewId);
                return ReviewDto.FromReview(review);
            }
            else
            {
                _logger.LogWarning("Review not found with ID: {ReviewId}", request.ReviewId);
                return null;
            }
        }
    }
}
