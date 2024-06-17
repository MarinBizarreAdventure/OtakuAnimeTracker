using OtakuTracker.Domain.Models;


namespace OtakuTracker.Application.Abstractions
{
    public interface IReviewsRepository
    {
        Review CreateReview(Review review);
        Review GetReviewById(int reviewId);
        List<Review> GetReviewsByUserId(int userId);
        List<Review> GetReviewsByAnimeId(int animeId);
        List<Review> GetReviewsByRating(decimal rating);
        void UpdateReview(Review review);
        void DeleteReview(int reviewId);
    }
}
