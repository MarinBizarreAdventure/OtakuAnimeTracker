﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Reviews.Commands
{
    public record CreateReview(
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        int? UserId,
    
        [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
        int? AnimeId,
    
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        int? Rating,
    
        [StringLength(500, ErrorMessage = "ReviewText must be at most 500 characters long.")]
        string? ReviewText,
    
        [DataType(DataType.Date, ErrorMessage = "ReviewDate must be a valid date.")]
        DateOnly? ReviewDate
    ) : IRequest<ReviewDto>;
    
    
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
                if (review.Rating > 10 || review.Rating < 1)
                {
                    var errorMessage = "Rating should be in this range [1,10]";
                    _logger.LogError(errorMessage);
                    throw new Exception(errorMessage);
                }
                
                var createdReview = await _unitOfWork.ReviewRepository.CreateReview(review);
                _logger.LogInformation("Review created successfully");
                return ReviewDto.FromReview(createdReview);
            }
            catch (Exception ex)
            {
                var errorMessage = "Failed to create review";
                _logger.LogError(ex, errorMessage);
                throw new Exception(errorMessage + ex.Message);
            }
        }
    }

}
