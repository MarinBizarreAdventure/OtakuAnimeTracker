using OtakuTracker.Application.Abstractions;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Infrastructure.Repositories;

public class ReviewRepository : IReviewsRepository
{
    private readonly AnimeDbContext _context;

    public ReviewRepository(AnimeDbContext context)
    {
        _context = context;
    }

    public Review CreateReview(Review review)
    {
        _context.Reviews.Add(review);
        _context.SaveChanges();
        return review;
    }

    public Review GetReviewById(int reviewId)
    {
        return _context.Reviews.Find(reviewId);
    }

    public List<Review> GetReviewsByUserId(int userId)
    {
        return _context.Reviews.Where(r => r.UserId == userId).ToList();
    }

    public List<Review> GetReviewsByAnimeId(int animeId)
    {
        return _context.Reviews.Where(r => r.AnimeId == animeId).ToList();
    }

    public List<Review> GetReviewsByRating(decimal rating)
    {
        return _context.Reviews.Where(r => r.Rating == rating).ToList();
    }

    public void UpdateReview(Review review)
    {
        _context.Reviews.Update(review);
        _context.SaveChanges();
        //return review;
    }

    public void DeleteReview(int reviewId)
    {
        var review = _context.Reviews.Find(reviewId);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}