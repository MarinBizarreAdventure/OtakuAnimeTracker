// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Reviews.Responses;
// using OtakuTracker.Domain.Models;
//
// namespace OtakuTracker.Application.Reviews.Commands
// {
//     public record CreateReview(int UserId, int AnimeId, decimal Rating, string ReviewText, DateTime ReviewDate) : IRequest<ReviewDto>;
//
//     public class CreateReviewHandler : IRequestHandler<CreateReview, ReviewDto>
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILogger<CreateReviewHandler> _logger;
//
//         public CreateReviewHandler(IUnitOfWork unitOfWork, ILogger<CreateReviewHandler> logger)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }
//
//         public async Task<ReviewDto> Handle(CreateReview request, CancellationToken cancellationToken)
//         {
//             _logger.LogInformation("Handling request to create review");
//
//             var review = new Review()
//             {
//                 UserId = request.UserId,
//                 AnimeId = request.AnimeId,
//                 Rating = request.Rating,
//                 ReviewText = request.ReviewText,
//                 ReviewDate = request.ReviewDate
//             };
//
//             await _unitOfWork.BeginTransactionAsync();
//
//             try
//             {
//                 var createdReview = _unitOfWork.ReviewRepository.CreateReview(review);
//                 await _unitOfWork.CommitTransactionAsync();
//                 _logger.LogInformation("Review created successfully");
//                 return ReviewDto.FromReview(createdReview);
//             }
//             catch (Exception ex)
//             {
//                 await _unitOfWork.RollbackTransactionAsync();
//                 _logger.LogError(ex, "Failed to create review");
//                 throw;
//             }
//         }
//     }
// }
