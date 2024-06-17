using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;

namespace OtakuTracker.Application.Reviews.Queries
{
    public record GetReviewsByUserId(int UserId) : IRequest<List<ReviewDto>>;

    public class GetReviewsByUserIdHandler : IRequestHandler<GetReviewsByUserId, List<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetReviewsByUserIdHandler> _logger;

        public GetReviewsByUserIdHandler(IUnitOfWork unitOfWork, ILogger<GetReviewsByUserIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewsByUserId request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to get reviews by user ID");

            try
            {
                var reviews = _unitOfWork.ReviewRepository.GetReviewsByUserId(request.UserId);
                var reviewDtos = reviews.Select(ReviewDto.FromReview).ToList();

                _logger.LogInformation("Retrieved {Count} reviews for user ID {UserId}", reviewDtos.Count, request.UserId);

                return await Task.FromResult(reviewDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get reviews by user ID {UserId}", request.UserId);
                throw;
            }
        }
    }
}
