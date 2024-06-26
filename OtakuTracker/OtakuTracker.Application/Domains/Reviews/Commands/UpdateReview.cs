﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Reviews.Commands
{
    public record UpdateReview(int ReviewId, string? ReviewText, int? Rating, DateOnly? ReviewDate) : IRequest<ReviewDto>;

    public class UpdateReviewHandler : IRequestHandler<UpdateReview, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateReviewHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateReviewHandler(IUnitOfWork unitOfWork, ILogger<UpdateReviewHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(UpdateReview request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request to update review");

            await _unitOfWork.BeginTransactionAsync();

            try
            {   
                var existingReview = await _unitOfWork.ReviewRepository.GetReviewById(request.ReviewId);
                if (existingReview == null)
                {
                    _logger.LogWarning("Review not found with ID: {ReviewId}", request.ReviewId);
                    throw new KeyNotFoundException($"Review with ID {request.ReviewId} not found.");
                }

                var review = _mapper.Map(request,existingReview);
               
                await _unitOfWork.ReviewRepository.UpdateReview(review);
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Review updated successfully");
                return ReviewDto.FromReview(review);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Failed to update review");
                throw;
            }
        }
    }
}
