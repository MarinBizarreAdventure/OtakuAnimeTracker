using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Reviews.Commands
{
    public record CreateReview(int? UserId, int? AnimeId, int? Rating, string? ReviewText, DateOnly? ReviewDate) : IRequest<ReviewDto>;

    public class CreateReviewHandler : IRequestHandler<CreateReview, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateReviewHandler> _logger;
        private readonly IMapper _mapper;


        public CreateReviewHandler(IUnitOfWork unitOfWork, ILogger<CreateReviewHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(CreateReview request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to create review");

            try
            {
                var review = _mapper.Map<Review>(request);
                var createdReview = await _unitOfWork.ReviewRepository.CreateReview(review);
                _logger.LogInformation("Review created successfully");
                return ReviewDto.FromReview(createdReview);
            }
            catch (Exception ex)
            {
                var errorMessage = "Failed to create review";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }

}
