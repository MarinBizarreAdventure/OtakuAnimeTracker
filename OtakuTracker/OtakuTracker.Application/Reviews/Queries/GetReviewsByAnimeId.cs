using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;

namespace OtakuTracker.Application.Reviews.Queries
{
    public record GetReviewsByAnimeId(int AnimeId) : IRequest<List<ReviewDto>>;

    public class GetReviewsByAnimeIdHandler : IRequestHandler<GetReviewsByAnimeId, List<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetReviewsByAnimeIdHandler> _logger;

        public GetReviewsByAnimeIdHandler(IUnitOfWork unitOfWork, ILogger<GetReviewsByAnimeIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewsByAnimeId request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling request to get reviews for anime with ID {request.AnimeId}");

            var reviews = _unitOfWork.ReviewRepository.GetReviewsByAnimeId(request.AnimeId);
            var reviewDtoList = reviews.Select(review => ReviewDto.FromReview(review)).ToList();

            _logger.LogInformation($"Found {reviewDtoList.Count} reviews for anime with ID {request.AnimeId}");

            return await Task.FromResult(reviewDtoList);
        }
    }
}
