using Microsoft.EntityFrameworkCore;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Infrastructure.Models;
using Review = OtakuTracker.Domain.Models.Review;

namespace OtakuTracker.Infrastructure.Repositories;

public class ReviewRepository : IReviewsRepository
{
    private readonly OtakutrackerContext _context;

    public ReviewRepository(OtakutrackerContext context)
    {
        _context = context;
    }

    public async Task<Review> CreateReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<Review> GetReviewById(int reviewId)
    {
        return await _context.Reviews.FindAsync(reviewId);
    }

    public async Task<List<Review>> GetReviewsByUserId(int userId)
    {
        return await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<List<Review>> GetReviewsByAnimeId(int animeId)
    {
        return await _context.Reviews.Where(r => r.AnimeId == animeId).ToListAsync();
    }

    public async Task<List<Review>> GetReviewsByRating(decimal rating)
    {
        return await _context.Reviews.Where(r => r.Rating == rating).ToListAsync();
    }

    public async Task UpdateReview(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteReview(int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}