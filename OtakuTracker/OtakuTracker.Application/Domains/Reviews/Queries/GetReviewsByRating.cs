// using MediatR;
// using Microsoft.Extensions.Logging;
// using OtakuTracker.Application.Abstractions;
// using OtakuTracker.Application.Reviews.Responses;
//
// namespace OtakuTracker.Application.Reviews.Queries
// {
//     public record GetReviewsByRating(decimal Rating) : IRequest<List<ReviewDto>>;
//
//     public class GetReviewsByRatingHandler : IRequestHandler<GetReviewsByRating, List<ReviewDto>>
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILogger<GetReviewsByRatingHandler> _logger;
//
//         public GetReviewsByRatingHandler(IUnitOfWork unitOfWork, ILogger<GetReviewsByRatingHandler> logger)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }
//
//         public async Task<List<ReviewDto>> Handle(GetReviewsByRating request, CancellationToken cancellationToken)
//         {
//             _logger.LogInformation("Handling request to get reviews by rating");
//
//             try
//             {
//                 var reviews = _unitOfWork.ReviewRepository.GetReviewsByRating(request.Rating);
//                 var reviewDtos = reviews.Select(review => ReviewDto.FromReview(review)).ToList();
//                 return await Task.FromResult(reviewDtos);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Failed to get reviews by rating");
//                 throw;
//             }
//         }
//     }
// }
