using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IReviewsRepository
    {
        Task<Review> CreateReview(Review review);
        Task<Review> GetReviewById(int reviewId);
        Task<List<Review>> GetReviewsByUserId(int userId);
        Task<List<Review>> GetReviewsByAnimeId(int animeId);
        Task<List<Review>> GetReviewsByRating(decimal rating);
        Task UpdateReview(Review review);
        Task DeleteReview(int reviewId);
    }
}
