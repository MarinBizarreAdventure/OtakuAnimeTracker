// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Reviews.Responses;
//
// namespace OtakuTracker.Application.Reviews.Commands
// {
//     public record UpdateReview(int ReviewId, decimal Rating, string ReviewText) : IRequest<ReviewDto>;
//
//     public class UpdateReviewHandler : IRequestHandler<UpdateReview, ReviewDto>
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILogger<UpdateReviewHandler> _logger;
//
//         public UpdateReviewHandler(IUnitOfWork unitOfWork, ILogger<UpdateReviewHandler> logger)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }
//
//         public async Task<ReviewDto> Handle(UpdateReview request, CancellationToken cancellationToken)
//         {
//             _logger.LogInformation("Handling request to update review");
//
//             var review = _unitOfWork.ReviewRepository.GetReviewById(request.ReviewId);
//             if (review == null)
//             {
//                 _logger.LogWarning($"Review with ID {request.ReviewId} not found");
//                 return null; // Or throw appropriate exception
//             }
//
//             review.Rating = request.Rating;
//             review.ReviewText = request.ReviewText;
//
//             _unitOfWork.ReviewRepository.UpdateReview(review);
//
//             await _unitOfWork.CommitTransactionAsync();
//
//             _logger.LogInformation("Review updated successfully");
//             return ReviewDto.FromReview(review);
//         }
//     }
// }
